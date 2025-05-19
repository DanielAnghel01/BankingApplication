using BankingApplication.Infrastructure;
using BankingApplication.Services.Interface;
using BankingApplication.Views.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankingApplication.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BankingDbContext _context;

        public ProfileService(UserManager<IdentityUser> userManager, BankingDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ProfileViewModel> GetUserProfileAsync(string identityUserId)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == identityUserId);

            if (user == null)
                return null;

            var accounts = await _context.BankAccounts
            .Where(a => a.UserId == identityUserId)
            .ToListAsync();

            var profile = new ProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                BankAccounts = accounts
            };

            return profile;
        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}
