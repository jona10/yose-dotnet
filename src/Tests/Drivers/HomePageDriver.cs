using FluentAssertions;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Tests.Drivers
{
    public class HomePageDriver
    {
        private HttpClient _client;

        public HomePageDriver(HttpClient _client)
        {
            this._client = _client;
        }

        public async void GreetsWith(string greeting)
        {
            var response = await _client.GetAsync("/");
            response.StatusCode.Should().Be(HttpStatusCode.OK, "because the resource should exist");

            var document = new HtmlDocument();
            document.Load(response.Content.ReadAsStreamAsync().Result);

            var nodes = document.QuerySelectorAll("#greeting");
            nodes.Should().NotBeEmpty("because the application should return an element containing the greeting");
            nodes.First().InnerHtml.Should().Be(greeting, "because the application should greet Yose");
        }
    }
}
