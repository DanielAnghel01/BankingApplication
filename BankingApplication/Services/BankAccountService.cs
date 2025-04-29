using BankingApplication.Infrastructure;
using BankingApplication.Models;
using BankingApplication.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace BankingApplication.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly BankingDbContext _context;

        public BankAccountService(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccount> CreateAccount(string accountHolderName, decimal initialDeposit, int UserId, string AccountType)
        {
            var account = new BankAccount
            {
                AccountNumber = GenerateAccountNumber(),
                AccountHolderName = accountHolderName,
                Balance = initialDeposit,
                CreatedAt = DateTime.UtcNow,
                UserId = UserId,
                AccountType = AccountType
            };

            _context.BankAccounts.Add(account);
            await _context.SaveChangesAsync();
            if (initialDeposit > 0)
            {
                _context.Transactions.Add(new Transaction
                {
                    BankAccountId = account.Id,
                    Amount = initialDeposit,
                    TransactionType = "Deposit",
                    TransactionDate = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<bool> Deposit(int accountId, decimal amount)
        {
            var account = await _context.BankAccounts.FindAsync(accountId);

            if (account == null || amount <= 0)
                return false;

            account.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                BankAccountId = accountId,
                Amount = amount,
                TransactionType = "Deposit",
                TransactionDate = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Withdraw(int accountId, decimal amount)
        {
            var account = await _context.BankAccounts.FindAsync(accountId);

            if (account == null || amount <= 0 || account.Balance < amount)
                return false;

            account.Balance -= amount;

            _context.Transactions.Add(new Transaction
            {
                BankAccountId = accountId,
                Amount = amount,
                TransactionType = "Withdrawal",
                TransactionDate = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            var fromAccount = await _context.BankAccounts.FindAsync(fromAccountId);
            var toAccount = await _context.BankAccounts.FindAsync(toAccountId);

            if (fromAccount == null || toAccount == null || amount <= 0 || fromAccount.Balance < amount)
                return false;

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                BankAccountId = fromAccountId,
                Amount = amount,
                TransactionType = "Transfer Out",
                TransactionDate = DateTime.UtcNow
            });

            _context.Transactions.Add(new Transaction
            {
                BankAccountId = toAccountId,
                Amount = amount,
                TransactionType = "Transfer In",
                TransactionDate = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BankAccount> GetAccountDetails(int accountId)
        {
            return await _context.BankAccounts
                .Include(a => a.User) // if you have a navigation property
                .FirstOrDefaultAsync(a => a.Id == accountId);
        }

        public async Task<List<Transaction>> GetTransactionHistory(int accountId)
        {
            return await _context.Transactions
                .Where(t => t.BankAccountId == accountId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }

        private string GenerateAccountNumber()
        {
            var random = new Random();
            return $"{random.Next(10000000, 99999999)}";
        }
    }
}
