using BankingApplication.Models;
using BankingApplication.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingApplication.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly IProfileService _profileService;
        private readonly UserManager<IdentityUser> _userManager;

        public BankAccountsController(IBankAccountService bankAccountService, IProfileService profileService, UserManager<IdentityUser> userManager)
        {
            _bankAccountService = bankAccountService;
            _profileService = profileService;
            _userManager = userManager;
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
            // Ideally you'd create a method like GetAllAccounts() in your service
            // But for now, assume you only have GetAccountDetails(int)
            // So no efficient "list" function yet.
            return View(); // Will need a list method later
        }

        // GET: BankAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var account = await _bankAccountService.GetAccountDetails(id.Value);

            if (account == null)
                return NotFound();

            return View(account);
        }

        // GET: BankAccounts/Create
        public async Task<IActionResult> Create()
        {
            var users = _userManager.Users.ToList(); 
            ViewBag.UserId = new SelectList(users, "Id", "FirstName");
            

            return View();
        }

        // POST: BankAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var createdAccount = await _bankAccountService.CreateAccount(bankAccount.AccountHolderName, bankAccount.Balance, userId, bankAccount.AccountType);
                return RedirectToAction(nameof(Details), new { id = createdAccount.Id });
            }

            return View();
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var account = await _bankAccountService.GetAccountDetails(id.Value);

            if (account == null)
                return NotFound();

            return View(account);
        }

        // POST: BankAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNumber,Balance,AccountType,UserId")] BankAccount bankAccount)
        {
            if (id != bankAccount.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // You don't have an UpdateAccount() yet.
                    // So either:
                    //  - add UpdateAccount() to service
                    //  - or rework here to manually update fields
                    // For now, I'll suggest adding an UpdateAccount method.
                    // await _bankAccountService.UpdateAccount(bankAccount);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    // handle exceptions here (optional logging)
                    throw;
                }
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var account = await _bankAccountService.GetAccountDetails(id.Value);

            if (account == null)
                return NotFound();

            return View(account);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // You don't have a DeleteAccount() method yet!
            // You would need to add it to your IBankAccountService and Service.
            return RedirectToAction(nameof(Index));
        }
    }
}
