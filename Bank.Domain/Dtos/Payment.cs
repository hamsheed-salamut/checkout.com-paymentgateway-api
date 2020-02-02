using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Domain.Dtos
{
    public class Payment
    {
        public long CardNumber { get; set; }
        public int Cvv { get; set; }
        public double Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public long AccountNumber { get; set; }

    }
}
