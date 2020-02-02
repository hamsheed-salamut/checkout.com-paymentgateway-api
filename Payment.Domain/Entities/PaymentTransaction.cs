using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Payment.Domain.Entities
{
    public class PaymentTransaction
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int MerchantId { get; set; }
        public long AccountNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public string CardNumber { get; set; }
        public string Status { get; set; }
    }
}
