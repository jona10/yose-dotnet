using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace Tests.Drivers
{
    public class ApiDriver
    {
        private HttpClient _client;

        public ApiDriver(HttpClient _client)
        {
            this._client = _client;
        }

        public async void IsAlive()
        {
            var response = await _client.GetAsync("/ping");
            response.StatusCode.Should().Be(HttpStatusCode.OK, "because the resource should exist");
            response.Content.Headers.GetValues("Content-Type").Should().Contain("application/json", "because the returned content should be formatted as JSON");

            var ping = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result, typeof(Ping));
            ping.Should().NotBeNull("because the response should be JSON formatted");
            ping.Should().BeOfType<Ping>().Which.Alive.Should().BeTrue("because the application should be alive");
        }

        private class Ping
        {
            public bool Alive { get; set; }
        }
    }
}
