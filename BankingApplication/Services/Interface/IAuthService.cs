namespace BankingApplication.Services.Interface
{
    public interface IAuthService
    {
        Task<string> Login(string username, string password);
        Task<string> Register(string username, string password);
        Task<bool> ValidateToken(string token);
        Task<string> RefreshToken(string token);
    }
}
