using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BankingApplication.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        public string? AccountNumber { get; set; }
        public string AccountHolderName { get; set; }

        public decimal Balance { get; set; }

        public string AccountType { get; set; } 
        public DateTime CreatedAt { get; set; } 

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserAccount? User { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }
    }
}
