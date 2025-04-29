using BankingApplication.Models;

namespace BankingApplication.Views.Dtos
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
    }
}
