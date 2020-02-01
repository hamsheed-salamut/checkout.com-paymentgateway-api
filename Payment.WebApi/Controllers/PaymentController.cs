using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        private readonly RestClient _client;
        private readonly string _bankUrl = "https://localhost:44302/";

        private Common.Logger.ILogger _logger;
        public PaymentController(IUserService userService, ICardService cardService, Common.Logger.ILogger logger)
        {
            _userService = userService;
            _cardService = cardService;
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] Payments paymentParam)
        {
            var request = new RestRequest("api/Bank/", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(paymentParam);
            //var accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1ODA1Mzc0NDksImV4cCI6MTU4MDcxMDI0OSwiaWF0IjoxNTgwNTM3NDQ5fQ._U61ISAacJ0 - LbLlLCLZ1EiWFAPpU_KPPf4UQ_wl18M";
            //var authenticator = new JwtAuthenticator(accessToken);
            //_client.Authenticator = authenticator;

            var response = _client.Execute<Payments>(request);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.Info("Information is logged...");
            return new string[] { "value1", "value2" };
        }
    }
}