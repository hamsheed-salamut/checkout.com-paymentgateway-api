using Payment.Domain.Entities;
using Payment.Interface.Interfaces;
using Payment.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Service.Services
{
    public class CardService : ICardService
    {
        private readonly IRepository<Card> _repository;
        private Common.Logger.ILogger _logger;
        public CardService(IRepository<Card> repository, Common.Logger.ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Card GetUserCardDetails(int userId)
        {
            var card = new Card();

            try
            {
                card = _repository.Single(x => x.UserId == userId);
            }
            catch(Exception ex)
            {
                _logger.Info(this.GetType().Name + ex.Message);
            }
            return card;
        }
    }
}
