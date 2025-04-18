﻿using BankingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankingApplication.Controllers
{
    public class TransferController : Controller
    {
        private readonly ILogger<TransferController> _logger;

        public TransferController(ILogger<TransferController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
