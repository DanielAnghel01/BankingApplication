using BankingApplication.Infrastructure;
using BankingApplication.Models;
using BankingApplication.Services.Interface;
using BankingApplication.Views.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BankingApplication.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly BankingDbContext _context;

        public BankAccountService(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccount> CreateAccount(string accountHolderName, decimal initialDeposit, string UserId, AccountType AccountType)
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

            await _context.BankAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
            if (initialDeposit > 0)
            {
                var transaction = new Transaction
                {
                    ToAccountId = account.Id,
                    Amount = initialDeposit,
                    Description = "Deposit",
                    Date = DateTime.UtcNow
                };
                await _context.Transactions.AddAsync(transaction);
            }
            
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<bool> Deposit(string accountNumber, decimal amount)
        {
            var account = await _context.BankAccounts.FirstOrDefaultAsync(e => e.AccountNumber == accountNumber);

            if (account == null || amount <= 0)
                return false;

            account.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                ToAccountId = account.Id,
                Amount = amount,
                Description = "Deposit",
                Date = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Withdraw(string accountNumber, decimal amount)
        {
            var account = await _context.BankAccounts.FirstOrDefaultAsync(e => e.AccountNumber == accountNumber);

            if (account == null || amount <= 0 || account.Balance < amount)
                return false;

            account.Balance -= amount;

            _context.Transactions.Add(new Transaction
            {
                FromAccountId = account.Id,
                Amount = amount,
                Description = "Withdrawal",
                Date = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            var fromAccount = await _context.BankAccounts.FirstOrDefaultAsync(e => e.AccountNumber == fromAccountId.ToString());
            var toAccount = await _context.BankAccounts.FirstOrDefaultAsync(e => e.AccountNumber == toAccountId.ToString());

            if (fromAccount == null || toAccount == null || amount <= 0 || fromAccount.Balance < amount)
                return false;

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            await _context.Transactions.AddAsync(new Transaction
            {
                FromAccountId = fromAccount.Id,
                ToAccountId = toAccount.Id,
                Amount = amount,
                Description = "Transfer Out",
                Date = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            await _context.Transactions.AddAsync(new Transaction
            {
                ToAccountId = toAccount.Id,
                FromAccountId = fromAccount.Id,
                Amount = amount,
                Description = "Transfer In",
                Date = DateTime.UtcNow
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
                .Where(t => t.FromAccountId == accountId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetAllUserTransactions(string userId)
        {
            // Get all the user's account IDs
            var userAccountIds = await _context.BankAccounts
                .Where(acc => acc.UserId == userId)
                .Select(acc => acc.Id)
                .ToListAsync();

            // Fetch all transactions involving any of the user's accounts
            var transactions = await _context.Transactions
                .Where(t => userAccountIds.Contains((int)t.FromAccountId) || userAccountIds.Contains((int)t.ToAccountId))
                .Include(t => t.FromAccount)
                .Include(t => t.ToAccount)
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            return transactions;
        }

        private string GenerateAccountNumber()
        {
            var random = new Random();
            return $"{random.Next(10000000, 99999999)}";
        }
    }
}
