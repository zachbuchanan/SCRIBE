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

        [HttpPost("CreateJobs")]        public string CreateJobs([FromBody] JobEditModel jobs)        {            return "#123";        }

        [HttpPost("AddNameAddress")]        public string AddCompanyNameAndAddress([FromBody] CompanyInfo info)        {            return "#456";        }

        [HttpPost("Sections")]
        public string ChooseSections([FromBody] SectionEditModel sections)        {            return "#789";        }

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

