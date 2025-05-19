using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingApplication.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int? FromAccountId { get; set; }
        public int? ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public BankAccount? FromAccount { get; set; }
        public BankAccount? ToAccount { get; set; }
    }
}
