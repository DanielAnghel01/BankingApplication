using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingApplication.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int DurationMonths { get; set; }
        public bool IsApproved { get; set; } = false;
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserAccount User { get; set; }
    }
}
