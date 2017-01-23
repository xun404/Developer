using AiursoftBase.Exceptions;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase
{
    public static class Values
    {
        public static string ServerAddress { get; private set; } = "http://api.aiursoft.obisoft.com.cn";
        public static string AppId { get; private set; } = "appid";
        public static string AppSecret { get; private set; } = "appsecret";
        public static IApplicationBuilder UseAiursoftAuthentication(this IApplicationBuilder app, string appId, string appSecret, string ServerAddress = "")
        {
            if (string.IsNullOrWhiteSpace(appId))
            {
                throw new NotAValidAiurArgumentException(nameof(appId));
            }
            if (string.IsNullOrWhiteSpace(appSecret))
            {
                throw new NotAValidAiurArgumentException(nameof(appSecret));
            }
            if (!string.IsNullOrWhiteSpace(ServerAddress))
            {
                Values.ServerAddress = ServerAddress;
            }
            AppId = appId;
            AppSecret = appSecret;
            return app;
        }
    }
}
