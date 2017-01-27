using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using AiursoftBase.Services;

namespace Developer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var AiurUrl = new AiurUrl("https://www.obisoft.com.cn", "/Home/Index", new
            //{
            //    appid = "aaaaa",
            //    appsecret = "bbbb"
            //});

            //Console.WriteLine(AiurUrl.ToString());

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
