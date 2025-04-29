using BankingApplication.Models;

namespace BankingApplication.Services.Interface
{
    public interface IBankAccountService
    {
        Task<BankAccount> CreateAccount(string accountHolderName, decimal initialDeposit, int UserId, string AccountType);
        Task<BankAccount> GetAccountDetails(int accountId);
        Task<bool> Deposit(int accountId, decimal amount);
        Task<bool> Withdraw(int accountId, decimal amount);
        Task<bool> Transfer(int fromAccountId, int toAccountId, decimal amount);
        Task<List<Transaction>> GetTransactionHistory(int accountId);
    }
}
