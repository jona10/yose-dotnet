using FluentAssertions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Linq;
using System.Net.Http;
using Yose;

namespace Tests
{
    public class Server
    {
        private IWebHost _host;

        public Server()
        {
            _host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Player>()
                .Build();
            _host.Start();
        }

        private string Address()
        {
            return ((IServerAddressesFeature)_host.ServerFeatures[typeof(IServerAddressesFeature)]).Addresses.First();
        }

        public void Stop()
        {
            _host.Dispose();
        }

        public HtmlDocument Get(string uri)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(Address()) })
            {
                var response = client.GetAsync("/").Result;
                response.IsSuccessStatusCode.Should().BeTrue("because the resource should exist");

                var document = new HtmlDocument();
                document.Load(response.Content.ReadAsStreamAsync().Result);

                return document;
            }
        }
    }
}