using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWorkerService.HostedService;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetDocController : ControllerBase
    {
        private List<Document> documents = new List<Document> 
        {
            new Document
            {
                Name = "Suri",
                Address = "Rayachoty",
                FullAddress =  "NG",
                MessageType = "MsgType",
                TargetSystem = "TargetSystem",
                Count = 0
            }
        };

        public int i = 0;

        private List<Document> Doc { get; set; }
        public GetDocController()
        {
            Doc = documents;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetDoc() 
        {
            i = i + 1;
            List<Document> documents = new List<Document>
            {
                new Document
                {
                    Name = "Suri",
                    Address = "Rayachoty",
                    FullAddress =  "NG",
                    MessageType = "MsgType",
                    TargetSystem = "TargetSystem",
                    ReceivedOn = DateTime.Now
                }
            };

            documents[0].Count = i ;
                return Ok(documents);
        }
    }
}