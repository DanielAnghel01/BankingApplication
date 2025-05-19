using BankingApplication.Models;
using BankingApplication.Views.Dtos;

namespace BankingApplication.Services.Interface
{
    public interface IBankAccountService
    {
        Task<BankAccount> CreateAccount(string accountHolderName, decimal initialDeposit, string UserId, AccountType AccountType);
        Task<BankAccount> GetAccountDetails(int accountId);
        Task<bool> Deposit(string accountNumber, decimal amount);
        Task<bool> Withdraw(string accountNumber, decimal amount);
        Task<bool> Transfer(int fromAccountId, int toAccountId, decimal amount);
        Task<List<Transaction>> GetTransactionHistory(int accountId);
        Task<List<Transaction>> GetAllUserTransactions(string userId);
    }
}
