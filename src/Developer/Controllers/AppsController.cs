using API.Models.AppsViewModels;
using Developer.Data;
using Developer.Models;
using Developer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AppsController : Controller
    {
        public UserManager<DeveloperUser> _userManager { get; protected set; }
        public SignInManager<DeveloperUser> _signInManager { get; protected set; }
        public IEmailSender _emailSender { get; protected set; }
        public ISmsSender _smsSender { get; protected set; }
        public ILogger _logger { get; protected set; }
        public DeveloperDbContext _dbContext { get; protected set; }

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

        [AllowAnonymous]
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
