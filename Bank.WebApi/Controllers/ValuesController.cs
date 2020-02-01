using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Domain.Dtos;
using Bank.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bank.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITransactionService _transactionSvc;

        public ValuesController(ITransactionService transactionSvc)
        {
            _transactionSvc = transactionSvc;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var payment = new Payment()
            {
                AccountNumber = 1,
                Amount = 25,
                CardNumber = 9854753651,
                Cvv = 321,
                ExpiryDate = DateTime.Now.AddDays(5)
            };

           // _transactionSvc.Debit(payment);

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
