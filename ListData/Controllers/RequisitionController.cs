using ListData.Models;
using ListData.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ListData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitionController : ControllerBase
    {
        private readonly IRequestionService _requestionService;
        public RequisitionController(IRequestionService requestionService)
        {
            _requestionService = requestionService;
        }
        // GET: api/<RequisitionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RequisitionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RequisitionController>
        [HttpPost]
        public List<ReturnList> Post([FromBody] List<RequistionApproved> values)
        {
            return _requestionService.ReturnLists(values);
        }

        // PUT api/<RequisitionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RequisitionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public List<PurchaseList> purchaseLists()
        {
            List<PurchaseList> purchaseList = new List<PurchaseList>()
            {
                new PurchaseList(){ itemCode= "M001", Qty=5},
                new PurchaseList(){ itemCode= "M002", Qty=10}
            };
            return purchaseList;
        }
    }


}
