namespace BankingApplication.Views.Dtos
{
    public class TransactionDisplayDto
    {
        public string Direction { get; set; } // "Sent" or "Received"
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
