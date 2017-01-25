using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Developer.Models;
using Developer.Services;
using Microsoft.Extensions.Logging;
using Developer.Data;
using Microsoft.EntityFrameworkCore;
using Developer.Models.AppsViewModels;

namespace API.Controllers
{
    public class ApiController : Controller
    {
        public readonly UserManager<DeveloperUser> _userManager;
        public readonly SignInManager<DeveloperUser> _signInManager;
        public readonly IEmailSender _emailSender;
        public readonly ISmsSender _smsSender;
        public readonly ILogger _logger;
        public readonly DeveloperDbContext _dbContext;

        public ApiController(
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
            _logger = loggerFactory.CreateLogger<ApiController>();
            _dbContext = _context;
        }

        public async Task<IActionResult> IsValidateApp(IsValidateAppAddressModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { message = "Wrong input.", code = -10 });
            }
            var _target = await _dbContext.Apps.SingleOrDefaultAsync(t => t.AppId == model.AppId);
            if (_target == null)
            {
                return Json(new { message = "Not found.", code = -4 });
            }
            if (_target.AppSecret != model.AppSecret)
            {
                return Json(new { message = "Wrong secret.", code = -1 });
            }
            return Json(new { message = "Currect", code = 0 });
        }
    }
}