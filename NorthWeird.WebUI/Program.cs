using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NorthWeird.Infrastructure.Logging;

namespace NorthWeird.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddProvider(new FileLoggerProvider(Path.Combine(Directory.GetCurrentDirectory(), $"logger-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.log")));
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
    }
}
