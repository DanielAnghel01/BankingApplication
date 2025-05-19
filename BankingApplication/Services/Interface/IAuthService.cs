using BankingApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace BankingApplication.Services.Interface
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterModel model);
        Task<SignInResult> LoginAsync(LoginModel model);
        Task LogoutAsync();
    }
}
