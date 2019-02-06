using System;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace OAuthServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "OAuth Server";
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
