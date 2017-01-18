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
}
