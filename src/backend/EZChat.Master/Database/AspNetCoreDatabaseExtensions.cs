using EZChat.Master.Database.QueryObject;
using EZChat.Master.Database.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace EZChat.Master.Database
{
    public static class AspNetCoreDatabaseExtensions
    {
        public static IServiceCollection AddEzChatDatabase(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>()
                    .AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<AppUserQueryObject>();

            return services;
        }
    }
}
