using EZChat.Master.Database.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace EZChat.Master.Database
{
    public static class AspNetCoreDatabaseServiceExtensions
    {
        public static IServiceCollection AddEzChatDatabase(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>()
                    .AddSingleton<IUserRepository, UserRepository>();

            return services;
        }
    }
}
