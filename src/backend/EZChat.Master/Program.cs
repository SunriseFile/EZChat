using System;
using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

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
                                       .ConfigureAppConfiguration(ConfigureConfiguration())
                                       .UseStartup<Startup>()
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
    }
}
