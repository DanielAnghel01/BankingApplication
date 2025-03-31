using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingApplication.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }
        public string TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public int BankAccountId { get; set; }
        [ForeignKey("BankAccountId")]
        public BankAccount BankAccount { get; set; }
    }
}
