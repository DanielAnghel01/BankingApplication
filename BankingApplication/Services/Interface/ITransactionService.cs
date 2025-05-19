namespace BankingApplication.Services.Interface
{
    public interface ITransactionService
    {
        Task<bool> TransferAsync(int fromAccountId, int toAccountId, decimal amount);
    }
}
