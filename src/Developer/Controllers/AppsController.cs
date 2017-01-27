using Developer.Data;
using Developer.Models;
using Developer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Developer.Models.AppsViewModels;
using AiursoftBase.Attributes;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            var _model = new AllAppsViewModel(_cuser)
            {
                MyApps = _dbContext.Apps.Where(t => t.CreaterId == _cuser.Id)
            };
            return View(_model);
        }

        [AiurForceAuth]
        public async Task<IActionResult> CreateApp()
        {
            var _cuser = await GetCurrentUserAsync();
            var _model = new CreateAppViewModel(_cuser);
            return View(_model);
        }
        [AiurForceAuth]
        [HttpPost]
        public async Task<IActionResult> CreateApp(CreateAppViewModel model)
        {
            var _cuser = await GetCurrentUserAsync();
            var _newApp = new App(_cuser.Id, model.AppName, model.AppDescription, model.AppCategory, model.AppPlatform)
            {
                CreaterId = _cuser.Id
            };
            _dbContext.Apps.Add(_newApp);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(AllApps));
        }

        [AiurForceAuth]
        public async Task<IActionResult> ViewApp(string id)
        {
            var _app = await _dbContext.Apps.SingleOrDefaultAsync(t=>t.AppId == id);
            var _cuser = await GetCurrentUserAsync();
            var _model = new ViewAppViewModel(_cuser);
            return View(_model);
        }

        private async Task<DeveloperUser> GetCurrentUserAsync()
        {
            return await _dbContext.Users.Include(t => t.MyApps).SingleOrDefaultAsync(t=>t.UserName == User.Identity.Name);
        }
    }
}
