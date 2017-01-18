using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Developer.Models;
using Developer.Services;
using Microsoft.Extensions.Logging;
using Developer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using API.Services;
using Newtonsoft.Json;
using System.Net;

namespace Developer.Controllers
{
    [AiurForceAuthException]
    public class AuthController : Controller, IAiurController
    {
        public UserManager<DeveloperUser> _userManager { get; protected set; }
        public SignInManager<DeveloperUser> _signInManager { get; protected set; }
        public IEmailSender _emailSender { get; protected set; }
        public ISmsSender _smsSender { get; protected set; }
        public ILogger _logger { get; protected set; }
        public DeveloperDbContext _dbContext { get; protected set; }

        public AuthController(
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
            _logger = loggerFactory.CreateLogger<AuthController>();
            _dbContext = _context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> IsValidateApp(IsValidateAppModel model)
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
        [AiurForceAuth]
        public IActionResult GoAuth()
        {
            return Redirect("/");
        }

        public async Task<IActionResult> AuthResult()
        {
            var _iController = this;
            var code = _iController.HttpContext.Request.Query["code"];
            var state = _iController.HttpContext.Request.Query["state"];
            if (!_iController.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(code))
            {
                var _accessToken = await OAuthService.AuthCodeToAccessTokenAsync(code);
                var _userinfo = await OAuthService.AccessTokenToUserInfo(AccessToken: _accessToken.access_token, openid: _accessToken.openid);

                var current = await _iController._userManager.FindByIdAsync(_userinfo.openid);
                if (current == null)
                {
                    current = new DeveloperUser
                    {
                        Id = _userinfo.openid,
                        nickname = _userinfo.nickname,
                        sex = _userinfo.sex,
                        headimgurl = _userinfo.headimgurl,
                        UserName = _userinfo.openid
                    };
                    var result = await _iController._userManager.CreateAsync(current);
                }
                else
                {
                    current.nickname = _userinfo.nickname;
                    current.sex = _userinfo.sex;
                    current.headimgurl = _userinfo.headimgurl;
                    await _iController._userManager.UpdateAsync(current);
                }
                await _iController._signInManager.SignInAsync(current, false);
            }
            return Redirect(state);
        }
    }

    public class IsValidateAppModel
    {
        [Required]
        public virtual string AppId { get; set; }
        [Required]
        public virtual string AppSecret { get; set; }
    }
    public interface IAiurController
    {
        UserManager<DeveloperUser> _userManager { get; }
        SignInManager<DeveloperUser> _signInManager { get; }
        DeveloperDbContext _dbContext { get; }
        ClaimsPrincipal User { get; }
        HttpContext HttpContext { get; }
    }
    public class AiurForceAuth : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var _iController = context.Controller as IAiurController;
            if (!_iController.User.Identity.IsAuthenticated)
            {
                throw new NotAiurSignedInException(_iController);
            }
        }
    }
    public class NotAiurSignedInException : Exception
    {
        public IAiurController controller { get; }
        public NotAiurSignedInException(IAiurController controller)
        {
            this.controller = controller;
        }
    }
    public class AiurForceAuthExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            switch (context.Exception.GetType().Name)
            {
                case nameof(NotAiurSignedInException):
                    var _iController = (context.Exception as NotAiurSignedInException).controller;
                    var request = _iController.HttpContext.Request;
                    var hostpath = $"{request.Scheme}://{request.Host}{request.Path}";
                    var authpath = $"{request.Scheme}://{request.Host}/Auth/AuthResult";
                    var url = OAuthService.GenerateAuthUrl(authpath,hostpath);
                    _iController.HttpContext.Response.Redirect(url);
                    break;
                default:
                    break;
            }
            context.ExceptionHandled = true;
        }
    }

    public static class OAuthService
    {
        public async static Task<AuthAccessToken> AuthCodeToAccessTokenAsync(string code)
        {
            var HTTPContainer = new HTTPService();
            var URL = $@"{Values.ServerAddress}/oauth/access_token?appid={
                Values.AppId}&secret={
                Values.AppSecret}&code={code}&grant_type=authorization_code";
            var result = await HTTPContainer.Get(URL);
            var JResult = JsonConvert.DeserializeObject<AuthAccessToken>(result);
            return JResult;
        }
        public async static Task<UserInfoViewModel> AccessTokenToUserInfo(string AccessToken, string openid)
        {
            var HTTPContainer = new HTTPService();
            var URL = $@"{Values.ServerAddress}/oauth/UserInfo?access_token={
                AccessToken}&openid={
                openid}&lang=en-US";

            var result = await HTTPContainer.Get(URL);
            var JResult = JsonConvert.DeserializeObject<UserInfoViewModel>(result);
            return JResult;
        }
        public static string GenerateAuthUrl(string Destination, string State = "null")
        {
            string result = Values.ServerAddress + $@"/oauth/authorize?appid={
             Values.AppId}&redirect_uri={
             WebUtility.UrlEncode(Destination)}&response_type=code&scope=snsapi_base&state={
             WebUtility.UrlEncode(State)}#aiursoft_redirect";
            return result;
        }
    }
    public class AuthAccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string openid { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }
    public class UserInfoViewModel
    {
        public virtual string openid { get; set; }
        public virtual string nickname { get; set; }
        public virtual string sex { get; set; }
        public virtual string headimgurl { get; set; }
    }
}