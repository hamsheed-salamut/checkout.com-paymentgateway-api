using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Dtos
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
    }
}
