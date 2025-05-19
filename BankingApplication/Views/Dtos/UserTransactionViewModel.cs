namespace BankingApplication.Views.Dtos
{
    public class UserTransactionViewModel
    {
        public string UserName { get; set; }
        public List<TransactionDisplayDto> Transactions { get; set; }
    }
}
