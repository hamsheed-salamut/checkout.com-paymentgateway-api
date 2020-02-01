using Payment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Service.Interfaces
{
    public interface ICardService
    {
        Card GetUserCardDetails(int userId);
    }
}
