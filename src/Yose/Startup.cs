using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Yose.Controllers;

namespace Yose
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouter(ConfigureRoutes(app, env));
        }

        private static IRouter ConfigureRoutes(IApplicationBuilder app, IWebHostEnvironment env)
        {
            return new RouteBuilder(app)
                .MapGet("ping", new PingController().Get)
                .Build();
        }
    }
}
