using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Developer.Models;
using Developer.Data;
using AiursoftBase.Services;
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
            var code = int.Parse(_controller.HttpContext.Request.Query["code"]);
            var state = _controller.HttpContext.Request.Query["state"];
            if (!_controller.User.Identity.IsAuthenticated && code > 0)
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
                        preferedLanguage = _userinfo.preferedLanguage,
                        accountCreateTime = _userinfo.accountCreateTime
                    };
                    var result = await _controller._userManager.CreateAsync(current);
                }
                else
                {
                    current.nickname = _userinfo.nickname;
                    current.sex = _userinfo.sex;
                    current.headimgurl = _userinfo.headimgurl;
                    current.preferedLanguage = _userinfo.preferedLanguage;
                    current.accountCreateTime = _userinfo.accountCreateTime;
                    await _controller._userManager.UpdateAsync(current);
                }
                await _controller._signInManager.SignInAsync(current, true);
            }
            return Redirect(state);
        }
    }
}