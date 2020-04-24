using System.Collections.Generic;
using Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        public string outputPathForGeneratedContract = "./";
        [HttpPost("CreateJobs")]        public string CreateJobs([FromBody] JobEditModel jobs)        {
            //We don't have this functionality in Generator

            return "#123";        }

        [HttpPost("AddNameAddress")]        public string AddCompanyNameAndAddress([FromBody] CompanyInfo info)        {
            //We don't have this functionality in Generator
            return "#456";        }

        [HttpPost("Sections")]
        public string ChooseSections([FromBody] SectionEditModel sections)        {
            //Call
            
            //Param 1: List of Strings, Param 2 Name of output Document

            List<string> file_list = new List<string>();            for (var i = 0; i < sections.Sections.Count; ++i)
            {
                if (sections.Sections[i].IsSelected)
                {
                    file_list.Add(sections.Sections[i].Value);
                }
            }
            //Hardcoded dubug fields for stitch
            List<KeyValuePair<string, string>> fieldInfo = new List<KeyValuePair<string, string>>();            fieldInfo.Add(new KeyValuePair<string, string>("TEST", "new value"));            API.Generator.Generator.Stitch(file_list, "Contract", outputPathForGeneratedContract, fieldInfo);            return "#789";        }

        [HttpPost("AddFileOutputPath")]
        public string AddFileOutputPath([FromBody] FilePath fileOutputPath)        {
            outputPathForGeneratedContract = fileOutputPath.path;
            return "#101112";        }

        [HttpPost("AddFileInputPath")]
        public List<CheckBoxEditModel> AddFileInputPathAndReturnFiles([FromBody] FilePath fileInputPath)        {
            var rawFileNames = API.Generator.Generator.UpdateFiles(@fileInputPath.path);
            List<CheckBoxEditModel> fileNames = new List<CheckBoxEditModel>();
            var idx = 0;
            foreach (var name in rawFileNames)
            {
                fileNames.Add(new CheckBoxEditModel(idx, name, false));
                ++idx;
            }

            return fileNames;
                    }

        //[HttpGet("GetFileNames")]
        //public List<CheckBoxEditModel> SendFileNamesToFrontEnd()
        //{
        //    //var rawFileNames = API.Generator.Generator.UpdateFiles(@"C:\Users\Zach\Desktop\SCRIBE\testfiles");
        //    var rawFileNames = API.Generator.Generator.UpdateFiles(@fileInput);
        //    List<CheckBoxEditModel> fileNames = new List<CheckBoxEditModel>();
        //    var idx = 0;
        //    foreach(var name in rawFileNames)
        //    {
        //        fileNames.Add(new CheckBoxEditModel(idx, name, false));
        //        ++idx;
        //    }

        //    return fileNames;
        //}
    }
}


