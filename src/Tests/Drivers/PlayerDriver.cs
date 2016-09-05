using FluentAssertions;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Net;
using System;

namespace Tests.Drivers
{
    public class PlayerDriver : IDisposable
    {
        private HttpClient _client;

        public PlayerDriver(HttpClient client)
        {
            _client = client;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public void GreetsYose()
        {
            var response = _client.GetAsync("/").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK, "because the resource should exist");

            var document = new HtmlDocument();
            document.Load(response.Content.ReadAsStreamAsync().Result);

            var nodes = document.QuerySelectorAll("#greeting");
            nodes.Should().NotBeEmpty("because the application should return an element containing the greeting");
            nodes.First().InnerHtml.Should().Be("Hello Yose", "because the application should greet Yose");
        }

        public void Pings()
        {
            var response = _client.GetAsync("/ping").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK, "because the resource should exist");
            response.Content.Headers.GetValues("Content-Type").Should().Contain("application/json", "because the returned content should be formatted as JSON");

            var ping = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result, typeof(Ping));
            ping.Should().NotBeNull("because the response should be JSON formatted");
            ping.Should().BeOfType<Ping>().Which.IsAlive.Should().BeTrue("because the application should be alive");
        }

        private class Ping
        {
            public bool IsAlive { get; set; }
        }
    }
}
