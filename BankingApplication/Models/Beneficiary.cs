using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingApplication.Models
{
    public class Beneficiary
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string AccountNumber { get; set; } 
        public string BankName { get; set; } 
        public string Nickname { get; set; } 
        public DateTime AddedOn { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserAccount User { get; set; }
    }
}
