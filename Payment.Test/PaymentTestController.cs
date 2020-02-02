using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Payment.Domain.Entities;
using Payment.Infrastructure.Context;
using Payment.Service.Interfaces;
using Payment.Service.Services;
using Payment.WebApi.Controllers;
using Payment.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Payment.Test
{
    public class PaymentTestController
    {
        private readonly Mock<IUserService> _userService;
        private readonly Mock<ICardService> _cardService;
        private readonly Mock<ITransactService> _transactService;
        private Mock<Common.Logger.ILogger> _logger;
     
        public PaymentTestController()
        {
            //var context = new PaymentContext(_dbContextOptions);
            _userService = new Mock<IUserService>();
            _cardService = new Mock<ICardService>();
            _transactService = new Mock<ITransactService>();
            _logger = new Mock<Common.Logger.ILogger>();

        }

        [Fact]
        public void User_Pay_OkObjectResult()
        {
            var controller = new PaymentController(_userService.Object, _cardService.Object,  _transactService.Object,  _logger.Object);
            var pay = new Payments()
            {
                AccountNumber = 2,
                Amount = 100,
                CardNumber = 4261392791756353,
                Cvv = 812,
                ExpiryDate = DateTime.Now.AddDays(5),
                Token = "token",
                MerchantAccountNumber = 3
            };

            var data = controller.Post(pay);

            Assert.IsType<OkObjectResult>(data);
        }
      
        [Fact]
        public void User_Authentication_BadRequestObjectResult()
        {
            var controller = new PaymentController(_userService.Object, _cardService.Object, _transactService.Object, _logger.Object);
            var user = new User()
            {
                Email = "test@test.com",
                Password = "test"
            };

            var data = controller.Authenticate(user);

            Assert.IsType<BadRequestObjectResult>(data);
        }

        #region Get By Merchant Id
        [Fact]
        public void Merchant_GetTransactionById_OkResult()
        {
            var controller = new PaymentController(_userService.Object, _cardService.Object, _transactService.Object, _logger.Object);

            var data = controller.Get(2);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Merchant_GetTransactionById_NotFoundResult()
        {
            var controller = new PaymentController(_userService.Object, _cardService.Object, _transactService.Object, _logger.Object);

            var data = controller.Get(0);

            Assert.IsType<OkObjectResult>(data);
        }
        #endregion
    }
}
