using Payment.Domain.Dtos;
using Payment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Service.Interfaces
{
    public interface ITransactService
    {
        PaymentResponse CreateTransaction(GatewayTransaction transaction);
        List<GatewayTransaction> GetMerchantTransaction(int id);
    }
}
