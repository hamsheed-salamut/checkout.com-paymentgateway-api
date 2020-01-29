using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Domain.Dtos
{
    public class BankResponse
    {
        public bool IsSuccess { get; set; }
        public Guid TransactionId { get; set; }
    }
}
