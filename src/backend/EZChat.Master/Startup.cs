﻿using System;

using EZChat.Master.Database;
using EZChat.Master.DataProtection;
using EZChat.Master.Identity;
using EZChat.Master.Middleware;
using EZChat.Master.Migrations;
using EZChat.Master.Swagger;

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
                    .AddEzChatIdentity(_config)
                    .AddEzChatMigrations(_config)
                    .AddEzChatSwagger()
                    .AddEzChatDataProtection(_config);

            services.AddMvc(ConfigureMvc());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseEzChatSwagger()
               .UseMiddleware<UnhandledExceptionMiddleware>()
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
    }
}
