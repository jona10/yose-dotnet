using FluentAssertions;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Linq;

namespace Tests.Drivers
{
    public class Player
    {
        private Server _server;

        public Player(Server server)
        {
            _server = server;
        }

        public void GreetsYose()
        {
            var document = _server.Get("/");
            var nodes = document.QuerySelectorAll("#hello-yose");
            nodes.Should().NotBeEmpty("because the application should return an element containing the greeting");
            nodes.First().InnerHtml.Should().Be("Hello Yose", "because the application should greet Yose");
        }
    }
}
