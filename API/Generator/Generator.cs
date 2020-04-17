using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Collections;
using System.IO.Compression;

//Only funcs needed to be called out UpdateFiles and Stitch

namespace API.Generator
{
    public class Generator
    {

        public static void UpdateFiles(string PATH)
        {
            char sep = Path.DirectorySeparatorChar;
            // be sure filedata directory exists
            if (!Directory.Exists("./filedata"))
                Directory.CreateDirectory("./filedata");

            // read in filenames and modification times of all files in PATH directory

            string[] files = Directory.GetFiles(PATH);
            string[] files_data = Directory.GetDirectories("./filedata");


            // scan data folder, and file that no longer exists or is outdated gets removed

            foreach (string foldername in files_data)
            {
                string filename = foldername.Split(sep).Last() + ".docx"; // filename without full path
                string filepath = "";

                if (File.Exists(PATH + "/" + filename))

                    filepath = PATH + "/" + filename;

                else // if the file is deleted delete the data
                {
                    Directory.Delete(foldername, true);
                    ColorWrite(ConsoleColor.Red, filename + " data was removed because the file no longer exists.");
                    continue;
                }

                if (Directory.GetLastWriteTime(foldername) < File.GetLastWriteTime(filepath))
                {
                    Directory.Delete(foldername, true); // The file is outdated.
                    ColorWrite(ConsoleColor.Red, filename + " data was removed because the file has been updated.");
                }
            }

            // extract/copy any files that do not exist into data folder

            foreach (string filepath in files)
            {
                string filename = Path.GetFileNameWithoutExtension(filepath.Split(sep).Last());
                if (!Directory.Exists("./filedata/" + filename))
                {
                    try
                    {
                        ZipFile.ExtractToDirectory(filepath, "./filedata/" + filename);

                        uniquify(filename);

                        ColorWrite(ConsoleColor.Green, "Copied data for file: " + filename + ".");

                    }
                    catch (System.IO.InvalidDataException e)
                    {
                        ColorWrite(ConsoleColor.Red, String.Format("The file {0} could not be opened.", filename));
                        throw e;
                    }


                }
            }

            ColorWrite(ConsoleColor.Green, "File updating completed.\n");

        }

        public static void Stitch(List<string> filenames, string outputname)
        {

            if (filenames.Count() == 0) //end immediately if filenames[0] doesn't exist.
            {
                ColorWrite(ConsoleColor.Red, "Document stitching failed: No files provided.");
                return;
            }

            for (int i = 0; i < filenames.Count(); ++i)
            {
                filenames[i] = Path.GetFileNameWithoutExtension(filenames[i]);
            }

            // delete previous stitched document

            if (Directory.Exists("./stitched"))
                Directory.Delete("./stitched", true);

            // if filenames[0] exists, copy the entire thing as stitched
            if (Directory.Exists("./filedata/" + filenames[0]))
            {
                DirectoryCopy("./filedata/" + filenames[0], "./stitched/", true);
            }

            //else error
            else
            {
                ColorWrite(ConsoleColor.Red, filenames[0] + " does not exist.");
                return;
            }

            //for filenames[1-n]... 
            for (int i = 1; i < filenames.Count(); ++i)
            {
                // if the file exists
                if (Directory.Exists("./filedata/" + filenames[i]))
                {

                    // combine document.xml
                    MergeDocumentXML("./stitched/word/document.xml", "./filedata/" + filenames[i] + "/word/document.xml");
                    CopyMedia("./stitched/word/media", "./filedata/" + filenames[i] + "/word/media");
                    MergeRels("./stitched/word/_rels/document.xml.rels", "./filedata/" + filenames[i] + "/word/_rels/document.xml.rels", filenames[i]);
                }



                // if the file doesnt exist error
                else
                {
                    ColorWrite(ConsoleColor.Red, filenames[i] + " does not exist.");
                    return;
                }
            }

            //zip it up as a docx

            if (File.Exists("./" + outputname + ".docx"))
                File.Delete("./" + outputname + ".docx");
            ZipFile.CreateFromDirectory("./stitched", "./" + outputname + ".docx");

            ColorWrite(ConsoleColor.Green, "File stitching completed.\n");

        }

        static void MergeDocumentXML(string path1, string path2)
        {
            using (FileStream fs1 = File.OpenRead(path1))
            using (FileStream fs2 = File.OpenRead(path2))
            {
                XElement xl1 = XElement.Load(fs1);
                XElement xl2 = XElement.Load(fs2);
                XContainer body = (XContainer)xl1.FirstNode;
                XContainer body2 = (XContainer)xl2.FirstNode;
                body.LastNode.AddAfterSelf(xl2.Nodes());

                fs1.Close();
                fs2.Close();
                xl1.Save("./stitched/word/document.xml");
            }

        }

        static void CopyMedia(string destination, string source)
        {
            //if there's a media folder in source...
            if (Directory.Exists(source))
            {
                DirectoryCopy(source, destination, true);
            }
            //else
            //    ColorWrite(ConsoleColor.Cyan, "no media to copy from " + source);

        }

        static void MergeRels(string destination, string source, string NamePrefix)
        {
            using (FileStream src = File.OpenRead(source))
            using (FileStream dst = File.OpenRead(destination))
            {
                XElement body = XElement.Load(dst);
                XElement body2 = XElement.Load(src);
                foreach (XElement n in body2.Elements())
                {
                    XAttribute Target = n.Attribute("Target");
                    if (Target == null) continue;
                    if (Target.Value.Contains(NamePrefix))
                    {
                        body.LastNode.AddAfterSelf(n);
                    }

                }
                src.Close();
                dst.Close();
                body.Save("./stitched/word/_rels/document.xml.rels");
            }

        }

        static void uniquify(string DocumentName)
        {
            string mediaPath = String.Format("./filedata/{0}/word/media", DocumentName);
            if (Directory.Exists(mediaPath))
            {

                //Rename all media files and track changes
                List<string> filenames =
                    new List<string>();

                string[] files = Directory.GetFiles(mediaPath);
                foreach (string filePath in files)
                {
                    string fileDir = Path.GetDirectoryName(filePath);
                    string fileName = Path.GetFileName(filePath);
                    System.IO.File.Move(filePath, String.Format("{0}/{1}_{2}", fileDir, DocumentName, fileName));
                    filenames.Add(fileName);
                }

                //Change rId and target in rels and track changes
                List<string> rids =
                    new List<string>();


                using (FileStream rels = File.OpenRead(String.Format("./filedata/{0}/word/_rels/document.xml.rels", DocumentName)))
                {
                    XElement body = XElement.Load(rels);
                    foreach (XElement node in body.Elements())
                    {
                        foreach (string name in filenames)
                        {
                            XAttribute rId = node.Attribute("Id");
                            XAttribute Target = node.Attribute("Target");
                            if (Target.Value.Contains(name))
                            {
                                string adjustedName = Target.Value.Replace("/", "/" + DocumentName + "_");
                                rids.Add(rId.Value);
                                node.SetAttributeValue("Id", DocumentName + "_" + rId.Value);
                                node.SetAttributeValue("Target", adjustedName);

                            }
                        }
                    }
                    rels.Close();
                    body.Save(String.Format("./filedata/{0}/word/_rels/document.xml.rels", DocumentName));
                }

                //Change rels in document.xml - wow this is really inefficient but it works
                using (FileStream docxml = File.OpenRead(String.Format("./filedata/{0}/word/document.xml", DocumentName)))
                {
                    XElement doc = XElement.Load(docxml);
                    IEnumerable<XElement> nodes = doc.Descendants();
                    foreach (XElement node in nodes)
                    {
                        IEnumerable<XAttribute> attrs = node.Attributes();
                        foreach (XAttribute attr in attrs)
                        {
                            foreach (string refnum in rids)
                            {
                                if (attr.Value == refnum)
                                {
                                    attr.SetValue(DocumentName + "_" + refnum);
                                }
                            }
                        }
                    }
                    docxml.Close();
                    doc.Save(String.Format("./filedata/{0}/word/document.xml", DocumentName));
                }
            }
        }



        static void ColorWrite(ConsoleColor c_, string s_)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = c_;
            Console.WriteLine(s_);
            Console.ForegroundColor = temp;
        }


        //Lifted from Microsoft 
        static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }

        }
    }
}
