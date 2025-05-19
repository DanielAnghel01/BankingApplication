using BankingApplication.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingApplication.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(IProfileService profileService, UserManager<IdentityUser> userManager)
        {
            _profileService = profileService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var identityUserId = _userManager.GetUserId(User); // Gets the current logged-in user's ID

            if (identityUserId == null)
            {
                return RedirectToAction("Login", "Account"); // fallback for non-authenticated access
            }

            var profile = await _profileService.GetUserProfileAsync(identityUserId);

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }
    }
}
