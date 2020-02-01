using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.WebApi.Models
{
    public class Payments
    {
        public long CardNumber { get; set; }
        public int Cvv { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double Amount { get; set; }
        public int AccountNumber { get; set; }
        public string Token { get; set; }
    }
}
