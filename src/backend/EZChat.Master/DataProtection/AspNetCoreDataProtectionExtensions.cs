using System;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EZChat.Master.DataProtection
{
    public static class AspNetCoreDataProtectionExtensions
    {
        public static IServiceCollection AddEzChatDataProtection(this IServiceCollection services, IConfiguration config)
        {
            services.AddDataProtection()
                    .SetApplicationName(config["App:Name"]);

            services.AddSingleton<IXmlRepository, DataProtectionXmlRepository>();

            var provider = services.BuildServiceProvider();

            services.Configure(ConfigureDataProtection(provider));

            return services;
        }

        private static Action<KeyManagementOptions> ConfigureDataProtection(ServiceProvider provider)
        {
            return options => options.XmlRepository = provider.GetRequiredService<IXmlRepository>();
        }
    }
}
