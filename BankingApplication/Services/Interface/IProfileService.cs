using BankingApplication.Models;
using BankingApplication.Views.Dtos;

namespace BankingApplication.Services.Interface
{
    public interface IProfileService
    {
        Task<ProfileViewModel> GetUserProfileAsync(int userId);
        Task<List<UserAccount>> GetAllUsers();
    }
}
