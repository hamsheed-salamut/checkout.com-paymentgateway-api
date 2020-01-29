using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Infrastructure.Context
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions options) : base(options)
        {

        }
        public BankContext()
        {

        }
        public DbSet<Account> Accounts { get; set; }
    }
}
