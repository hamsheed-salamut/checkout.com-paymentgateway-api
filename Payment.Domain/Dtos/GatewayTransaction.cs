using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Dtos
{
    public class GatewayTransaction
    {
        public Guid Id { get; set; }
        public int MerchantId { get; set; }
        public long AccountNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public string CardNumber { get; set; }
        public string Status { get; set; }
        public long MerchantAccountNumber { get; set; }
    }
}
