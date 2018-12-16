using System;
using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using Serilog;
using Serilog.Events;

namespace EZChat.Master
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost().Run();
        }

        private static IWebHost BuildWebHost()
        {
            return new WebHostBuilder().UseContentRoot(Directory.GetCurrentDirectory())
                                       .UseKestrel()
                                       .UseStartup<Startup>()
                                       .ConfigureAppConfiguration(ConfigureConfiguration())
                                       .UseSerilog(ConfigureLogger())
                                       .Build();
        }

        private static Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureConfiguration()
        {
            return (context, config) =>
            {
                var env = context.HostingEnvironment;

                config.AddJsonFile("appsettings.json", false, false)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, false);
            };
        }

        private static Action<WebHostBuilderContext, LoggerConfiguration> ConfigureLogger()
        {
            return (context, logger) =>
            {
                logger.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                      .MinimumLevel.Override("System", LogEventLevel.Warning)
                      .Enrich.FromLogContext()
                      .WriteTo.Console()
                      .WriteTo.RollingFile("logs/events-{Date}.log")
                      .ReadFrom.Configuration(context.Configuration);
            };
        }
    }
}
