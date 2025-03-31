using System.ComponentModel.DataAnnotations;

namespace BankingApplication.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public decimal Rate { get; set; } 
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
