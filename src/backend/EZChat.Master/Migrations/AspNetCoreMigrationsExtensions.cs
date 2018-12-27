using System;
using System.Reflection;

using FluentMigrator.Runner;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EZChat.Master.Migrations
{
    public static class AspNetCoreMigrationsExtensions
    {
        public static IServiceCollection AddEzChatMigrations(this IServiceCollection services, IConfiguration config)
        {
            services.AddFluentMigratorCore()
                    .ConfigureRunner(ConfigureMigrationRunner(config));

            return services;
        }

        private static Action<IMigrationRunnerBuilder> ConfigureMigrationRunner(IConfiguration config)
        {
            return builder => builder.AddPostgres()
                                     .WithGlobalConnectionString(config.GetConnectionString("DefaultConnection"))
                                     .ScanIn(Assembly.GetExecutingAssembly())
                                     .For.Migrations();
        }
    }
}
