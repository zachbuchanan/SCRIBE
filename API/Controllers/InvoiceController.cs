using System.Collections.Generic;
using Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        //[HttpPost("GetInvoiceId")]
        //public string GetInvoiceId([FromBody] Job job)
        //{
        //    return "#123";
        //}

        [HttpPost("CreateJobs")]        public string CreateJobs([FromBody] JobEditModel jobs)        {            //We don't have this functionality in Generator                        return "#123";        }

        [HttpPost("AddNameAddress")]        public string AddCompanyNameAndAddress([FromBody] CompanyInfo info)        {            //We don't have this functionality in Generator            return "#456";        }

        [HttpPost("Sections")]
        public string ChooseSections([FromBody] SectionEditModel sections)        {
            //Call
            API.Generator.Generator.UpdateFiles("/Users/Zach/Desktop/Scribe/testfiles");
            //Param 1: List of Strings, Param 2 Name of output Document

            List<string> file_list = new List<string>();            for(var i =  0; i < sections.Sections.Count; ++i)
            {
                if(sections.Sections[i].IsSelected)
                {
                    file_list.Add(sections.Sections[i].Value);
                }
            }            API.Generator.Generator.Stitch(file_list, "Contract");            return "#789";        }

        [HttpGet("GetFileNames")]
        public List<string> SendFileNamesToFrontEnd()
        {
            List<string> fileNames = new List<string>();
            fileNames.Add("Header");
            fileNames.Add("Legal");
            fileNames.Add("Sales");
            fileNames.Add("Simple");
            fileNames.Add("Complicated");
            return fileNames;
        }
    }
}

//TO DO 
//setting the path POST api -> choose path -> send to backend


//["Section1", "..."] gets passed to API
//API turns into JSON
//API will send to front end
//

