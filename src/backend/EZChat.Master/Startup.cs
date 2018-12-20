﻿using System;
using System.Reflection;

using EZChat.Master.Database;
using EZChat.Master.Identity;
using EZChat.Master.Swagger;

using FluentMigrator.Runner;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EZChat.Master
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEzChatDatabase()
                    .AddEzChatIdentity(_config);

            services.AddMvc(ConfigureMvc());

            services.AddFluentMigratorCore()
                    .ConfigureRunner(ConfigureMigrationRunner());

            services.AddEzChatSwagger();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseEzChatSwagger()
               .UseAuthentication()
               .UseMvc();
        }

        private Action<MvcOptions> ConfigureMvc()
        {
            return options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            };
        }

        private Action<IMigrationRunnerBuilder> ConfigureMigrationRunner()
        {
            return builder => builder.AddPostgres()
                                     .WithGlobalConnectionString(_config.GetConnectionString("DefaultConnection"))
                                     .ScanIn(Assembly.GetExecutingAssembly())
                                     .For.Migrations();
        }
    }
}
