using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Domain.Dtos
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
