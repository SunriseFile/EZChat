using System;
using System.IO;

using FluentMigrator.Runner;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Events;

namespace EZChat.Master
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = CreateConfiguration();

            ConfigureLogger(config);

            try
            {
                var host = BuildWebHost(config);

                Log.Information("Starting application...");

                using (var scope = host.Services.CreateScope())
                {
                    scope.ServiceProvider
                         .GetRequiredService<IMigrationRunner>()
                         .MigrateUp();
                }

                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHost BuildWebHost(IConfiguration config)
        {
            return new WebHostBuilder()
                   .UseKestrel()
                   .UseStartup<Startup>()
                   .UseConfiguration(config)
                   .UseSerilog()
                   .Build();
        }

        private static IConfiguration CreateConfiguration()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var appsettings = "appsettings";

            if (string.IsNullOrWhiteSpace(env))
            {
                throw new Exception("Environment variable ASPNETCORE_ENVIRONMENT is required");
            }

            return new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile($"{appsettings}.json", false, false)
                   .AddJsonFile($"{appsettings}.{env}.json", false, false)
                   .Build();
        }

        private static void ConfigureLogger(IConfiguration config)
        {
            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                         .MinimumLevel.Override("System", LogEventLevel.Warning)
                         .Enrich.FromLogContext()
                         .WriteTo.Console()
                         .WriteTo.RollingFile("logs/events-{Date}.log")
                         .ReadFrom.Configuration(config)
                         .CreateLogger();
        }
    }
}
