using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merchant.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace Merchant.WebUI.Controllers
{
    public class GatewayController : Controller
    {
        private readonly RestClient _client;
        private readonly string _gatewayUrl = "https://localhost:44359/";
        private static string _userToken;

        public GatewayController()
        {
            _client = new RestClient { BaseUrl = new System.Uri(_gatewayUrl) };
        }
        public IActionResult Index(double amount)
        {
            var model = new UserViewModel()
            {
                Amount = amount
            };

            return View(model);
        }

        public IActionResult Authenticate(UserViewModel vm)
        {
            var request = new RestRequest("api/payment/authenticate", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(vm);

            var response = _client.Execute<UserViewModel>(request);
            var model = new UserViewModel();
            dynamic responseObj = JsonConvert.DeserializeObject(response.Content.ToString());
            _userToken = responseObj.token;
            if (responseObj.token != null)
            {
                model = new UserViewModel()
                {
                    CardNumber = responseObj.cardNumber,
                    ExpiryDate = responseObj.expiryDate,
                    Amount = vm.Amount,
                    Cvv = responseObj.cvv
                };
                return View("~/Views/Gateway/Checkout.cshtml", model);
            }
            return View("~/Views/Gateway/Error.cshtml");
        }

        public IActionResult Pay(UserViewModel vm)
        {
            var request = new RestRequest("api/payment/", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(vm);
            var accessToken = _userToken;
            var authenticator = new JwtAuthenticator(accessToken);
            _client.Authenticator = authenticator;
            var response = _client.Execute<dynamic>(request);

            if (!response.IsSuccessful)
            {
                return View("~/Views/Gateway/Unauthorized.cshtml");
            }

            return View("~/Views/Gateway/Success.cshtml");
        }
    }
}