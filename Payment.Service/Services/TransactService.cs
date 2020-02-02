using Payment.Domain.Dtos;
using Payment.Domain.Entities;
using Payment.Interface.Interfaces;
using Payment.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payment.Service.Services
{
    public class TransactService : ITransactService
    {
        private readonly IRepository<PaymentTransaction> _repository;
        private Common.Logger.ILogger _logger;
        public TransactService(IRepository<PaymentTransaction> repository, Common.Logger.ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public PaymentResponse CreateTransaction(GatewayTransaction transaction)
        {
            var response = new PaymentResponse();

            try
            {
                var paymentTransaction = new PaymentTransaction()
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = transaction.AccountNumber,
                    Amount = transaction.Amount,
                    CardNumber = transaction.CardNumber,
                    MerchantId = transaction.MerchantId,
                    TransactionDate = transaction.TransactionDate,
                    Status = "success"
                };

                _repository.Insert(paymentTransaction);
                _repository.Save();

                response.Id = Guid.NewGuid();
                response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _logger.Error(this.GetType().Name + ex.Message);
            }
            return response;
        }
        public List<GatewayTransaction> GetMerchantTransaction(int id)
        {
            var transaction = new List<GatewayTransaction>();

            try
            {
                var query = _repository.FindBy(x => x.MerchantId == id);

                transaction = query.AsEnumerable().Select(item =>
                                new GatewayTransaction
                                {   Id = item.Id,
                                    Amount = item.Amount,
                                    CardNumber = item.CardNumber,
                                    Status = item.Status,
                                    TransactionDate = item.TransactionDate,
                                    AccountNumber = item.AccountNumber,
                                    MerchantId = item.MerchantId
                                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(this.GetType().Name + ex.Message);
            }
            return transaction;
        }
    }
}
