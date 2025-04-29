using BankingApplication.Infrastructure;
using BankingApplication.Services.Interface;
using BankingApplication.Views.Dtos;
using Microsoft.EntityFrameworkCore;
using BankingApplication.Models;

namespace BankingApplication.Services
{
    public class ProfileService : IProfileService
    {
        private readonly BankingDbContext _context;

        public ProfileService(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<ProfileViewModel> GetUserProfileAsync(int userId)
        {
            var user = await _context.UserAccounts.FindAsync(userId);
            if (user == null)
            {
                return null;
            }

            var accounts = await _context.BankAccounts
                .Where(a => a.UserId == userId)
                .ToListAsync();

            var profile = new ProfileViewModel
            {
                UserName = user.FirstName,
                BankAccounts = accounts
            };

            return profile;
        }

        public async Task<List<UserAccount>> GetAllUsers()
        {
            return await _context.UserAccounts.ToListAsync();
        }
    }
}
