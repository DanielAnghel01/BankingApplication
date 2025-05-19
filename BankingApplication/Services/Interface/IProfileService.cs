using BankingApplication.Models;
using BankingApplication.Views.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BankingApplication.Services.Interface
{
    public interface IProfileService
    {
        Task<ProfileViewModel> GetUserProfileAsync(string identityUserId);
        Task<List<IdentityUser>> GetAllUsers();
    }
}
