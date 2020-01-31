using Bank.Domain.Dtos;
using Bank.Domain.Entities;
using Bank.Infrastructure.Repositories;
using Bank.Interface.Interfaces;
using Bank.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Account> _repository;
        public TransactionService(IRepository<Account> repository)
        {
            _repository = repository;
        }

        public BankResponse Debit(Payment payment)
        {
            var response = new BankResponse();
            
            try
            {
                var account = _repository.Single(x => x.CardNumber == payment.CardNumber && x.Cvv == payment.Cvv);

                if (account != null)
                {
                    if ((account.Balance > payment.Amount) && (account.ExpiryDate >= payment.ExpiryDate))
                    {
                        var newBalance = account.Balance - payment.Amount;
                        account.Balance = newBalance;

                        _repository.Update(account);
                        _repository.Save();

                        response.IsSuccess = true;
                        response.TransactionId = Guid.NewGuid();
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.TransactionId = Guid.Empty;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public BankResponse Credit(Payment payment)
        {
            var response = new BankResponse();

            try
            {
                var account = _repository.Single(x => x.AccountNumber == payment.AccountNumber);

                if (account != null)
                {
                    account.Balance = account.Balance + payment.Amount;

                    _repository.Update(account);
                    _repository.Save();

                    response.IsSuccess = true;
                    response.TransactionId = Guid.NewGuid();
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}
