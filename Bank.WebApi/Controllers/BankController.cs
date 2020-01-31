using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Domain.Dtos;
using Bank.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly ITransactionService _transactionSvc;

        public BankController(ITransactionService transactionSvc)
        {
            _transactionSvc = transactionSvc;
        }

        // POST api/bank
        [HttpPost]
        public IActionResult Post([FromBody] Payment payment)
        {
            var response = new BankResponse();

            var debit = _transactionSvc.Debit(payment);

            if (debit.IsSuccess)
                response = _transactionSvc.Credit(payment);

            return Ok(response);
        }
    }
}