﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Yose
{
    public class Player
    {
        private IWebHost _host;

        public string Uri()
        {
            return ((IServerAddressesFeature)_host.ServerFeatures[typeof(IServerAddressesFeature)]).Addresses.First();
        }

        public void Start(IConfiguration configuration)
        {
            _host = new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseKestrel()
                .UseStartup<Player>()
                .Build();

            _host.Start();
        }

        public void Stop()
        {
            _host.Dispose();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                return context.Response.WriteAsync("<h1 id='hello-yose'>Hello Yose</h1>");
            });
        }
    }
}