using BankingApplication.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BankingApplication.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<IActionResult> Index()
        {
            int userId = 1;

            var profile = await _profileService.GetUserProfileAsync(userId);

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }
    }
}
