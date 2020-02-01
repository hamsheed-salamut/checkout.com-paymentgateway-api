using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Payment.Domain.Entities;
using Payment.Infrastructure.Context;
using Payment.Interface.Interfaces;
using Payment.Security.Helpers;
using Payment.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Payment.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly AppSettings _appSettings;
        private PaymentContext context;

        public UserService(PaymentContext context)
        {
            this.context = context;
        }

        public UserService(IRepository<User> repository, IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string email, string password)
        {
            var user = new User();

            try
            {
                user = _repository.Single(x => x.Email == email && x.Password == password);

                if (user == null)
                    return null;

                // authentication successful to generate a jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                //remove password before returning
                user.Password = null;
            }
            catch (Exception ex)
            {

            }
            return user;
        }
    }
}
