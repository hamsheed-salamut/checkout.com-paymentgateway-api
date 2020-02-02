using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merchant.WebUI.ViewModels
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public long CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }

        public double Amount { get; set; }
        public int Cvv { get; set; }
    }
}
