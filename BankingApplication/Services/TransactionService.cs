using BankingApplication.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BankingApplication.Models;
using BankingApplication.Services.Interface;

namespace BankingApplication.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankingDbContext _context;
        public TransactionService(BankingDbContext context)
        {
            _context = context;
        }
        public async Task<bool> TransferAsync(int fromAccountId, int toAccountId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Transfer amount must be greater than zero.");

            var fromAccount = await _context.BankAccounts.FindAsync(fromAccountId);
            var toAccount = await _context.BankAccounts.FindAsync(toAccountId);

            if (fromAccount == null || toAccount == null)
                throw new Exception("One or both accounts not found.");

            if (fromAccount.Balance < amount)
                throw new Exception("Insufficient balance in the source account.");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;

                // Optional: log transaction
                var transferLog = new Transaction
                {
                    FromAccountId = fromAccountId,
                    ToAccountId = toAccountId,
                    Amount = amount,
                    Date = DateTime.UtcNow,
                    Description = $"Transfer from Account {fromAccountId} to {toAccountId}"
                };

                _context.Transactions.Add(transferLog);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
