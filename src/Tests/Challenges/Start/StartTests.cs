using System.Net.Http;
using System.Threading.Tasks;
using Tests.Assertions;
using Tests.Fixtures;
using Xunit;
using Yose;

namespace Tests.Challenges.Start
{
    public class StartTests : IClassFixture<TestServerFixture<Startup>>
    {
        private readonly HttpClient _client;

        public StartTests(TestServerFixture<Startup> server)
        {
            _client = server.Client;
        }

        [Fact]
        public async Task Greets()
        {
            var response = await _client.GetAsync("/");
            response.Should().BeOk();
            response.Content.Should().Contain("Hello Yose");
        }

        [Fact]
        public async Task IsAlive()
        {
            var response = await _client.GetAsync("/ping");
            response.Should().BeOk();
            response.Content.Headers.Should().Contain("Content-Type").And.Be("application/json");
            response.Content.Should().Be("{\"alive\":true}");
        }

        [Fact]
        public async Task LinksToRepository()
        {
            var response = await _client.GetAsync("/");
            response.Should().BeOk();
            response.Content.Should().Contain("<a href=\"https://github.com/jona10/yose-dotnet\" target=\"_blank\">Github</a>");
        }
    }
}
