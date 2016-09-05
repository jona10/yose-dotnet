using FluentAssertions;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tests.Assertions;

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
            var document = await GetHomePage();

            var nodes = document.QuerySelectorAll("#greeting");
            nodes.Should().HaveCount(1, "because the application should implement a single element containing the greeting");
            nodes.First().InnerHtml.Should().Be(greeting, "because the application should greet Yose");
        }

        public async void LinksToRepository(string linkToRepository)
        {
            var document = await GetHomePage();

            var nodes = document.QuerySelectorAll("#repository-link");
            nodes.Should().HaveCount(1 , "because the application should implement a single element linking to the source code repository");

            var node = nodes.First();
            node.Should().BeAnAnchor();
            node.Should().HaveAttribute("href").Which.Should().HaveValue(linkToRepository, "because the application should link to the source code repository");
            node.Should().HaveAttribute("target").Which.Should().HaveValue("_blank", "because the application should open the source code repository in a new page");
        }

        private async Task<HtmlDocument> GetHomePage()
        {
            var response = await _client.GetAsync("/");
            response.StatusCode.Should().Be(HttpStatusCode.OK, "because the resource should exist");

            var document = new HtmlDocument();
            document.Load(response.Content.ReadAsStreamAsync().Result);

            return document;
        }
    }
}
