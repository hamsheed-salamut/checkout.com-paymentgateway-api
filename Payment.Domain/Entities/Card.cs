using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Payment.Domain.Entities
{
    public class Card
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long CardNumber { get; set; }
        public int Cvv { get; set; }
        public long AccountNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int UserId { get; set; }

        public string Token { get; set; }
    }
}
