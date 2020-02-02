using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Merchant.WebUI.Models;

namespace Merchant.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var products = new Products
            {
                ProductList = new List<Products>()
                {
                    new Products { Name = "Product A", Price = 100 },
                    new Products { Name = "Product B", Price = 200 }
                }
            };

            return View(products);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
