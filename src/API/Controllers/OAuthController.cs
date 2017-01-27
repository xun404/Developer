using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using API.Models.OAuthViewModels;
using API.Data;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AiursoftBase.Services;
using AiursoftBase.Models;

namespace API.Controllers
{
    public class OAuthController : Controller
    {
        private readonly UserManager<APIUser> _userManager;
        private readonly SignInManager<APIUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly APIDbContext _dbContext;

        public OAuthController(
            UserManager<APIUser> userManager,
            SignInManager<APIUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            APIDbContext _context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<OAuthController>();
            _dbContext = _context;
        }

        public async Task<IActionResult> Authorize(AuthorizeAddressModel model)
        {
            var cuser = await GetCurrentUserAsync();
            if (cuser != null)
            {
                var pack = await cuser.GeneratePack(_dbContext, model.appid);
                var url = AddCode(model.redirect_uri, pack.Code, model.state);
                return Redirect(url);
            }
            if (ModelState.IsValid)
            {
                var _viewModel = new AuthorizeViewModel();
                _viewModel.ToRedirect = model.redirect_uri;
                _viewModel.State = model.state;
                _viewModel.AppId = model.appid;
                _viewModel.Scope = model.scope;
                _viewModel.ResponseType = model.response_type;
                return View(_viewModel);
            }
            return View();
        }
        //http://localhost:62631/oauth/authorize?appid=appid&redirect_uri=http%3A%2F%2Flocalhost%3A55771%2FAuth%2FAuthResult&response_type=code&scope=snsapi_base&state=http%3A%2F%2Flocalhost%3A55771%2FAuth%2FGoAuth#aiursoft_redirect
        [HttpPost]
        public async Task<IActionResult> Authorize(AuthorizeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var cuser = await GetCurrentUserAsync(model.Email);
                    var pack = await cuser.GeneratePack(_dbContext, model.AppId);
                    var url = AddCode(model.ToRedirect, pack.Code, model.State);
                    return Redirect(url);
                }
                else if (result.RequiresTwoFactor)
                {
                    throw new NotImplementedException();
                }
                else if (result.IsLockedOut)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Register(AuthorizeAddressModel model)
        {
            if (ModelState.IsValid)
            {
                var _viewModel = new RegisterViewModel();
                _viewModel.ToRedirect = model.redirect_uri;
                _viewModel.State = model.state;
                _viewModel.AppId = model.appid;
                _viewModel.Scope = model.scope;
                _viewModel.ResponseType = model.response_type;
                return View(_viewModel);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new APIUser { UserName = model.Email, Email = model.Email, nickname = model.Email.Split('@')[0] };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    var cuser = await GetCurrentUserAsync(model.Email);
                    var pack = await cuser.GeneratePack(_dbContext, model.AppId);
                    var url = AddCode(model.ToRedirect, pack.Code, model.State);
                    return Redirect(url);
                }
                AddErrors(result);
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return Json(new { message = "Successfully signed out!", code = 0 });
        }

        public async Task<IActionResult> Access_token(Access_tokenAddressModel model)
        {
            var AppState = await OAuthService.CorrectAppAsync(model.AppId, model.AppSecret);
            if (AppState.code != 0)
            {
                return Json(AppState);
            }
            var _targetPack = await _dbContext.OAuthPack.SingleOrDefaultAsync(t => t.Code == model.Code);
            if (_targetPack == null)
            {
                return Json(new AiurProtocal { message = "Invalid Code.", code = -2 });
            }
            if (_targetPack.ApplyAppId != model.AppId)
            {
                return Json(new AiurProtocal { message = "The app granted code is not the app granting access token!", code = -3 });
            }
            var _newAccessToken = new AccessToken
            {
                OAuthPack = _targetPack,
                PackId = _targetPack.OAuthPackId,
                Value = (DateTime.Now.ToString() + model.AppId).GetMD5()
            };
            _dbContext.AccessToken.Add(_newAccessToken);
            await _dbContext.SaveChangesAsync();

            var _model = new AuthAccessToken
            {
                access_token = _newAccessToken.Value,
                openid = _targetPack.UserId,
                expires_in = 7200,
                refresh_token = "not implemented!",
                scope = "scope"
            };
            return Json(_model);
        }

        public async Task<IActionResult> UserInfo(UserInfoAddressModel model)
        {
            var Target = await _dbContext
                .AccessToken
                .Include(t => t.OAuthPack)
                .SingleOrDefaultAsync(t => t.Value == model.access_token);

            if (Target == null)
            {
                return Json(new AiurProtocal { message = "Invalid Access Token!", code = -1 });
            }
            if (Target.OAuthPack.UserId != model.openid)
            {
                return Json(new AiurProtocal { message = "Invalid Open Id!", code = -2 });
            }
            var _user = await _userManager.FindByIdAsync(model.openid);
            var _viewModel = new UserInfoViewModel
            {
                openid = _user.Id,
                headimgurl = _user.headimgurl,
                nickname = _user.nickname,
                sex = _user.sex,
                preferedLanguage = _user.preferedLanguage
            };
            return Json(_viewModel);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private async Task<APIUser> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
        private async Task<APIUser> GetCurrentUserAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        private string AddCode(string SourceUrl, int Code, string state)
        {
            var url = new Uri(SourceUrl);
            string result = $@"{url.Scheme}://{url.Host}:{url.Port}{url.AbsolutePath}?code={
                WebUtility.UrlEncode(Code.ToString())}&state={
                WebUtility.UrlEncode(state)}";
            return result;
        }
    }
}