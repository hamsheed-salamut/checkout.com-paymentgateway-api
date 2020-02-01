using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment.Domain.Entities;
using Payment.Infrastructure.Context;
using Payment.Service.Services;
using Payment.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Payment.Test
{
    public class PaymentTestController
    {
        private readonly UserService _userService;
        public static DbContextOptions<PaymentContext> _dbContextOptions { get; }
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PaymentDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static PaymentTestController()
        {
            _dbContextOptions = new DbContextOptionsBuilder<PaymentContext>().UseSqlServer(connectionString).Options;
        }

        public PaymentTestController()
        {
            var context = new PaymentContext(_dbContextOptions);
            _userService = new UserService(context);
        }

        [Fact]
        public void User_Authenticate_OkResult()
        {
            var controller = new PaymentController(_userService);
            var user = new User()
            {
                Email = "hamsheed@gmail.com",
                Password = "test"
            };

            var data = controller.Authenticate(user);

            Assert.IsType<OkObjectResult>(data);
        }
    }
}
