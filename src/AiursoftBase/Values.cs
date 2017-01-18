using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase
{
    public static class Values
    {
        public static string ServerAddress { get; } = "http://api.aiursoft.obisoft.com.cn";
        public static string AppId { get; private set; } = "appid";
        public static string AppSecret { get; private set; } = "appsecret";
        public static IApplicationBuilder UseAiursoftAuthentication(this IApplicationBuilder app, string appId, string appSecret)
        {
            AppId = appId;
            AppSecret = appSecret;
            return app;
        }
    }
}
