using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bank.Domain.Entities
{
    public class Account
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long AccountNumber { get; set; }
        public long CardNumber { get; set; }
        public int Cvv { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double Balance { get; set; }
    }
}
