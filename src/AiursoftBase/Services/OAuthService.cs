using AiursoftBase.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace AiursoftBase.Services
{

    public static class OAuthService
    {
        public async static Task<AuthAccessToken> AuthCodeToAccessTokenAsync(int code)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "/oauth/access_token", new Access_tokenAddressModel
            {
                AppId = Values.DeveloperSiteAppId,
                AppSecret = Values.DeveloperSiteAppSecret,
                Code = code,
                grant_type = "authorization_code"
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<AuthAccessToken>(result);
            return JResult;
        }
        public async static Task<UserInfoViewModel> AccessTokenToUserInfo(string AccessToken, string openid)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "/oauth/UserInfo", new UserInfoAddressModel
            {
                access_token = AccessToken,
                openid = openid,
                lang = "en-US"
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<UserInfoViewModel>(result);
            return JResult;
        }
        public static AiurUrl GenerateAuthUrl(string Destination, string State = "null")
        {
            var url = new AiurUrl(Values.ApiServerAddress, "/oauth/authorize", new AuthorizeAddressModel
            {
                appid = Values.DeveloperSiteAppId,
                redirect_uri = Destination,
                response_type = "code",
                scope = "snsapi_base",
                state = State
            });
            return url;
        }
        public async static Task<AiurProtocal> CorrectAppAsync(string AppId, string AppSecret)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.DeveloperServerAddress, "/api/IsValidateApp", new IsValidateAppAddressModel
            {
                AppId = AppId,
                AppSecret = AppSecret
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<AiurProtocal>(result);
            return JResult;
        }

    }
}
