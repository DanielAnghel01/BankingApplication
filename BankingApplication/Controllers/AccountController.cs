using BankingApplication.Infrastructure;
using BankingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankingApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly BankingDbContext _context;

        public AccountController(ILogger<AccountController> logger, BankingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Account/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [Route("Account/Login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Account/ResetPassword")]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
