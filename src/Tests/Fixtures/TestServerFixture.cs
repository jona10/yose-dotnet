using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net.Http;

namespace Tests.Fixtures
{
    public class TestServerFixture<TStartup> : IDisposable where TStartup : class
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TestServerFixture()
        {
            // Modern .NET 8 simplified content root detection
            var builder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment("Development")
                .UseStartup<TStartup>();

            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }

        public HttpClient Client => _client;

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}
