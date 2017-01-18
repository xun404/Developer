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
using Newtonsoft.Json;
using System.Net;
using AiursoftBase.Services;
using AiursoftBase.Models;
using AiursoftBase.Attributes;

namespace Developer.Controllers
{
    [AiurForceAuthException]
    public class AuthController : Controller
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
                await _iController._signInManager.SignInAsync(current, true);
            }
            return Redirect(state);
        }
    }
}