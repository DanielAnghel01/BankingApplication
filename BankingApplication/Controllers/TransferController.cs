using BankingApplication.Infrastructure;
using BankingApplication.Models;
using BankingApplication.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BankingApplication.Controllers
{
    public class TransferController : Controller
    {
        private readonly ILogger<TransferController> _logger;
        private readonly IBankAccountService _bankAccountService;
        private readonly BankingDbContext _context;

        public TransferController(ILogger<TransferController> logger, IBankAccountService bankAccountService, BankingDbContext context)
        {
            _logger = logger;
            _bankAccountService = bankAccountService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accounts = await _context.BankAccounts
                                 .Where(a => a.UserId == userId)
                                 .ToListAsync();

            // Prepare SelectList items for the dropdown
            var accountSelectList = accounts.Select(a => new SelectListItem
            {
                Value = a.AccountNumber,
                Text = $"{a.AccountNumber} - {a.AccountHolderName}"
            }).ToList();

            ViewBag.Accounts = accountSelectList;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            try
            {
                await _bankAccountService.Transfer(fromAccountId, toAccountId, amount);
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(); // or return to the transfer form with errors
            }
        }

        [HttpGet]
        public async Task<IActionResult> Deposit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accounts = await _context.BankAccounts
                                 .Where(a => a.UserId == userId)
                                 .ToListAsync();

            // Prepare SelectList items for the dropdown
            var accountSelectList = accounts.Select(a => new SelectListItem
            {
                Value = a.AccountNumber,
                Text = $"{a.AccountNumber} - {a.AccountHolderName}"
            }).ToList();

            ViewBag.Accounts = accountSelectList;
            return View();
        }

        // POST: Process Deposit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(string fromAccountId, decimal amount)
        {
            if (amount <= 0)
            {
                ModelState.AddModelError(string.Empty, "Amount must be greater than zero.");
                return View();
            }

            var success = await _bankAccountService.Deposit(fromAccountId, amount);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Deposit failed. Please check the account details.");
                return View();
            }

            TempData["SuccessMessage"] = "Deposit successful!";
            return RedirectToAction("Index", "Transactions"); // or wherever you want to redirect
        }

        // GET: Show Withdraw form
        [HttpGet]
        public async Task<IActionResult> Withdraw()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accounts = await _context.BankAccounts
                                 .Where(a => a.UserId == userId)
                                 .ToListAsync();

            // Prepare SelectList items for the dropdown
            var accountSelectList = accounts.Select(a => new SelectListItem
            {
                Value = a.AccountNumber,
                Text = $"{a.AccountNumber} - {a.AccountHolderName}"
            }).ToList();

            ViewBag.Accounts = accountSelectList;
            return View();
        }

        // POST: Process Withdrawal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(string fromAccountId, decimal amount)
        {
            if (amount <= 0)
            {
                ModelState.AddModelError(string.Empty, "Amount must be greater than zero.");
                return View();
            }

            var success = await _bankAccountService.Withdraw(fromAccountId, amount);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Withdrawal failed. Check your balance and account details.");
                return View();
            }

            TempData["SuccessMessage"] = "Withdrawal successful!";
            return RedirectToAction("Index", "Transactions"); // or wherever you want to redirect
        }
    }
}
