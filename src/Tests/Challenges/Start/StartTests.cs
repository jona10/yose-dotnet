using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tests.Fixtures;
using Xunit;
using Yose;

namespace Tests.Challenges
{
    public class StartTests : IClassFixture<TestServerFixture<Startup>>
    {
        private HttpClient _client;

        public StartTests(TestServerFixture<Startup> server)
        {
            _client = server.Client;
        }

        [Fact]
        public async Task Greets()
        {
            var response = await _client.GetAsync("/");
            response.StatusCode.Should().Be(HttpStatusCode.OK, "because the resource should exist");

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Hello Yose", "because the server should greet");
        }

        [Fact]
        public async Task IsAlive()
        {
            var response = await _client.GetAsync("/ping");
            response.StatusCode.Should().Be(HttpStatusCode.OK, "because the resource should exist");
            response.Content.Headers.GetValues("Content-Type").Should().Contain("application/json", "because the returned content should be formatted as JSON");

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be("{\"alive\":true}", "because the server should be alive");
        }

        [Fact]
        public async Task LinksToRepository()
        {
            var response = await _client.GetAsync("/");
            response.StatusCode.Should().Be(HttpStatusCode.OK, "because the resource should exist");

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("<a href=\"https://github.com/jona10/yose-dotnet\" target=\"_blank\">Github</a>", "because ");
        }
    }
}
