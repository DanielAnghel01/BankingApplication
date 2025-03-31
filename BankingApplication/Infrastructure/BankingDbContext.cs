using BankingApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BankingApplication.Infrastructure
{


    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) { }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
