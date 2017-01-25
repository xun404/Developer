using Developer.Data;
using Developer.Models;
using Developer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Developer.Models.AppsViewModels;
using AiursoftBase.Attributes;
using System.Threading.Tasks;

namespace API.Controllers
{
    [AiurForceAuthException]
    public class AppsController : Controller
    {
        private readonly UserManager<DeveloperUser> _userManager;
        private readonly SignInManager<DeveloperUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly DeveloperDbContext _dbContext;

        public AppsController(
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
            _logger = loggerFactory.CreateLogger<AppsController>();
            _dbContext = _context;
        }
        [AiurForceAuth]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Home";
            var _cuser = await GetCurrentUserAsync();
            var _model = new IndexViewModel(_cuser);
            return View(_model);
        }
        [AiurForceAuth]
        public async Task<IActionResult> AllApps()
        {
            ViewData["Title"] = "All Apps";
            var _cuser = await GetCurrentUserAsync();
            var _model = new AllAppsViewModel(_cuser);
            return View(_model);
        }

        private async Task<DeveloperUser> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
