using EZChat.Master.Database;
using EZChat.Master.Identity;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EZChat.Master
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEzChatDatabase()
                    .AddEzChatIdentity();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
    }
}
