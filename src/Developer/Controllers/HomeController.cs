using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Developer.Data;
using Developer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Developer.Models;

namespace Developer.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<DeveloperUser> _userManager { get; protected set; }
        public SignInManager<DeveloperUser> _signInManager { get; protected set; }
        public IEmailSender _emailSender { get; protected set; }
        public ISmsSender _smsSender { get; protected set; }
        public ILogger _logger { get; protected set; }
        public DeveloperDbContext _dbContext { get; protected set; }

        public HomeController(
            UserManager<DeveloperUser> userManager,
            SignInManager<DeveloperUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            DeveloperDbContext _context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _dbContext = _context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
