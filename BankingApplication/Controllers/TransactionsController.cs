using BankingApplication.Models;
using BankingApplication.Services;
using BankingApplication.Services.Interface;
using BankingApplication.Views.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankingApplication.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionService _transactionService;
        private readonly IBankAccountService _bankAccountService;
        private readonly UserManager<IdentityUser> _userManager;

        public TransactionsController(ILogger<TransactionsController> logger, ITransactionService transactionService, IBankAccountService bankAccountService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _transactionService = transactionService;
            _bankAccountService = bankAccountService;
            _userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var transactions = await _bankAccountService.GetAllUserTransactions(user.Id);

            var userAccountIds = transactions
                .Select(t => t.FromAccount?.UserId == user.Id ? t.FromAccount.Id : t.ToAccount?.Id)
                .ToList();

            var viewModel = new UserTransactionViewModel
            {
                UserName = user.UserName,
                Transactions = transactions.Select(t => new TransactionDisplayDto
                {
                    Direction = t.FromAccount?.UserId == user.Id ? "Sent" : "Received",
                    FromAccountNumber = t.FromAccount?.AccountNumber,
                    ToAccountNumber = t.ToAccount?.AccountNumber,
                    Amount = t.Amount,
                    Date = t.Date,
                    Description = t.Description
                }).ToList()
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        
    }
}
