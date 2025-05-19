using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using BankingApplication.Views.Dtos;


namespace BankingApplication.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }

        public string? AccountNumber { get; set; }
        public string AccountHolderName { get; set; }

        public decimal Balance { get; set; }

        public AccountType AccountType { get; set; } 
        public DateTime CreatedAt { get; set; } 

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }
    }
}
