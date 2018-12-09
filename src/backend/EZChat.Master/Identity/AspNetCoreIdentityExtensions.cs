using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EZChat.Master.Identity
{
    public static class AspNetCoreIdentityExtensions
    {
        public static IServiceCollection AddEzChatIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>();
            services.Configure(ConfigureIdentity());

            services.AddTransient<IUserStore<AppUser>, AppUserStore>()
                    .AddTransient<IRoleStore<AppRole>, AppRoleStore>();

            return services;
        }

        private static Action<IdentityOptions> ConfigureIdentity()
        {
            return options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            };
        }
    }
}
