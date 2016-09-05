using System.Net.Http;
using System;

namespace Tests.Drivers
{
    public class YoseDriver : IDisposable
    {
        private HttpClient _client;

        public YoseDriver(HttpClient client)
        {
            _client = client;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public HomePageDriver Home()
        {
            return new HomePageDriver(_client);
        }

        public ApiDriver Api()
        {
            return new ApiDriver(_client);
        }
    }
}
