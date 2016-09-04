﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            _host = BuildWebHost(configuration);
            _host.Start();
        }

        public void Run(IConfiguration configuration)
        {
            _host = BuildWebHost(configuration);
            _host.Run();
        }

        private static IWebHost BuildWebHost(IConfiguration configuration)
        {
            return new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseKestrel()
                .UseStartup<Player>()
                .Build();
        }

        public void Stop()
        {
            _host.Dispose();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                if (context.Request.Path.Equals("/ping"))
                {
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(new Ping { IsAlive = true }, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }));
                }

                return context.Response.WriteAsync("<h1 id='hello-yose'>Hello Yose</h1>");
            });
        }

        private class Ping
        {
            public bool IsAlive;
        }
    }
}
