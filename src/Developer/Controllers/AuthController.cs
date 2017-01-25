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
        public readonly UserManager<DeveloperUser> _userManager;
        public readonly SignInManager<DeveloperUser> _signInManager;
        public readonly DeveloperDbContext _dbContext;

        public AuthController(
            UserManager<DeveloperUser> userManager,
            SignInManager<DeveloperUser> signInManager,
            DeveloperDbContext _context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = _context;
        }

        [AiurForceAuth]
        public IActionResult GoAuth()
        {
            return Redirect("/");
        }


        public async Task<IActionResult> AuthResult()
        {
            var _controller = this;
            var code = _controller.HttpContext.Request.Query["code"];
            var state = _controller.HttpContext.Request.Query["state"];
            if (!_controller.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(code))
            {
                var _accessToken = await OAuthService.AuthCodeToAccessTokenAsync(code);
                var _userinfo = await OAuthService.AccessTokenToUserInfo(AccessToken: _accessToken.access_token, openid: _accessToken.openid);

                var current = await _controller._userManager.FindByIdAsync(_userinfo.openid);
                if (current == null)
                {
                    current = new DeveloperUser
                    {
                        Id = _userinfo.openid,
                        nickname = _userinfo.nickname,
                        sex = _userinfo.sex,
                        headimgurl = _userinfo.headimgurl,
                        UserName = _userinfo.openid,
                        preferedLanguage = _userinfo.preferedLanguage
                    };
                    var result = await _controller._userManager.CreateAsync(current);
                }
                else
                {
                    current.nickname = _userinfo.nickname;
                    current.sex = _userinfo.sex;
                    current.headimgurl = _userinfo.headimgurl;
                    current.preferedLanguage = _userinfo.preferedLanguage;
                    await _controller._userManager.UpdateAsync(current);
                }
                await _controller._signInManager.SignInAsync(current, true);
            }
            return Redirect(state);
        }
    }
}