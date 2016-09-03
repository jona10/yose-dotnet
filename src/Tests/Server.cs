using FluentAssertions;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Yose;

namespace Tests
{
    public class Server
    {
        private Player _host;

        public Server()
        {
            var configuration = new Dictionary<string, string> {
                {"server.urls", "http://localhost:9000"}
            };

            _host = new Player();
            _host.Start(new ConfigurationBuilder().AddInMemoryCollection(configuration).Build());
        }

        public void Stop()
        {
            _host.Stop();
        }

        public HtmlDocument Get(string uri)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_host.Uri()) })
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