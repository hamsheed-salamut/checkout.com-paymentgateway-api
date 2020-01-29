using Bank.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Service.Interfaces
{
    public interface ITransactionService
    {
        BankResponse Credit(Payment payment);
        BankResponse Debit(Payment payment);
    }
}
