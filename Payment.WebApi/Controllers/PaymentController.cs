using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.Common.Utilities;
using Payment.Domain.Dtos;
using Payment.Domain.Entities;
using Payment.Service.Interfaces;
using Payment.WebApi.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace Payment.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICardService _cardService;
        private readonly ITransactService _transactService;

        private readonly RestClient _client;
        private readonly string _bankUrl = "https://localhost:44302/";

        private Common.Logger.ILogger _logger;
        public PaymentController(IUserService userService, ICardService cardService, ITransactService transactService, Common.Logger.ILogger logger)
        {
            _userService = userService;
            _cardService = cardService;
            _transactService = transactService;
            _client = new RestClient { BaseUrl = new System.Uri(_bankUrl) };
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User userParam)
        {
            var user = _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
   
            Card userCardDetails = _cardService.GetUserCardDetails(user.Id);
            userCardDetails.Token = user.Token;

            return Ok(userCardDetails);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Payments paymentParam)
        {
            var request = new RestRequest("api/Bank/", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            paymentParam.AccountNumber = 3; // merchant account num

            request.AddJsonBody(paymentParam);

            var response = _client.Execute<Payments>(request);

            dynamic responseObj = JsonConvert.DeserializeObject(response.Content.ToString());

            if ((bool)responseObj.isSuccess)
            {
                var transact = new GatewayTransaction()
                {
                    AccountNumber = paymentParam.AccountNumber,
                    Amount = paymentParam.Amount,
                    CardNumber = CardMasker.Mask(paymentParam.CardNumber.ToString()),
                    Status = "success",
                    TransactionDate = DateTime.Now,
                    MerchantId = 2,          
                };

                _transactService.CreateTransaction(transact);
            }

            return Ok((bool)responseObj.isSuccess);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            var transactionList = _transactService.GetMerchantTransaction(id.Value);

            return Ok(transactionList);
        }
    }
}