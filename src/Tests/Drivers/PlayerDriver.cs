using FluentAssertions;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Linq;
using System;
using System.Net.Http;
using Yose;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Tests.Drivers
{
    public class PlayerDriver
    {
        private Player _player;

        public PlayerDriver(Player player)
        {
            var configuration = new Dictionary<string, string> {
                {"server.urls", "http://localhost:9001"}
            };

            _player = player;
            _player.Start(new ConfigurationBuilder().AddInMemoryCollection(configuration).Build());
        }

        public void GreetsYose()
        {
            using (var client = CreateClient())
            {
                var response = client.GetAsync("/").Result;
                response.IsSuccessStatusCode.Should().BeTrue("because the resource should exist");

                var document = new HtmlDocument();
                document.Load(response.Content.ReadAsStreamAsync().Result);

                var nodes = document.QuerySelectorAll("#hello-yose");
                nodes.Should().NotBeEmpty("because the application should return an element containing the greeting");
                nodes.First().InnerHtml.Should().Be("Hello Yose", "because the application should greet Yose");
            }
        }

        public void Pings()
        {
            using (var client = CreateClient())
            {
                var response = client.GetAsync("/ping").Result;
                response.IsSuccessStatusCode.Should().BeTrue("because the resource should exist");
                response.Content.Headers.GetValues("Content-Type").Should().Contain("application/json", "because the returned content should be formatted as JSON");

                var ping = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result, typeof(Ping));
                ping.Should().NotBeNull("because the response should be JSON formatted");
                ping.Should().BeOfType<Ping>().Which.IsAlive.Should().BeTrue("because the application should be alive");
            }
        }

        private HttpClient CreateClient()
        {
            return new HttpClient { BaseAddress = new Uri(_player.Uri()) };
        }

        private class Ping
        {
            public bool IsAlive;
        }
    }
}
