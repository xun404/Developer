using AiursoftBase.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AiursoftBase.Services
{
    public static class OAuthService
    {
        public async static Task<AuthAccessToken> AuthCodeToAccessTokenAsync(string code)
        {
            var HTTPContainer = new HTTPService();
            var URL = $@"{Values.ApiServerAddress}/oauth/access_token?{
                nameof(Values.DeveloperSiteAppId)}       =   {WebUtility.UrlEncode(Values.DeveloperSiteAppId)}&{
                nameof(Values.DeveloperSiteAppSecret)}   =   {WebUtility.UrlEncode(Values.DeveloperSiteAppSecret)}&{
                nameof(code)}               =   {WebUtility.UrlEncode(code)}&grant_type=authorization_code";
            var result = await HTTPContainer.Get(URL);
            var JResult = JsonConvert.DeserializeObject<AuthAccessToken>(result);
            return JResult;
        }
        public async static Task<UserInfoViewModel> AccessTokenToUserInfo(string AccessToken, string openid)
        {
            var HTTPContainer = new HTTPService();
            var URL = $@"{Values.ApiServerAddress}/oauth/UserInfo?access_token={
                WebUtility.UrlEncode(AccessToken)}&{nameof(openid)}={
                openid}&lang=en-US";

            var result = await HTTPContainer.Get(URL);
            var JResult = JsonConvert.DeserializeObject<UserInfoViewModel>(result);
            return JResult;
        }
        public static string GenerateAuthUrl(string Destination, string State = "null")
        {
            string result = Values.ApiServerAddress + $@"/oauth/authorize?appid={
             Values.DeveloperSiteAppId}&redirect_uri={
             WebUtility.UrlEncode(Destination)}&response_type=code&scope=snsapi_base&state={
             WebUtility.UrlEncode(State)}#aiursoft_redirect";
            return result;
        }
        public async static Task<AiurProtocal> CorrectAppAsync(string AppId, string AppSecret)
        {
            var HTTPContainer = new HTTPService();
            var URL = $@"{Values.DeveloperServerAddress}/api/IsValidateApp?AppId={
                WebUtility.UrlEncode(AppId)}&AppSecret={
                WebUtility.UrlEncode(AppSecret)}";
            var result = await HTTPContainer.Get(URL);
            var JResult = JsonConvert.DeserializeObject<AiurProtocal>(result);
            return JResult;
        }

    }
}
