using Microsoft.AspNetCore.Hosting;
using Tests.Drivers;
using Xunit;
using Yose;

namespace Tests.Challenges
{
    public class StartTests
    {
        private PlayerDriver _driver;
        private IWebHost _host;

        public StartTests()
        {
            _host = new WebHostBuilder().UseKestrel().UseStartup<Startup>().Build();
            _driver = new PlayerDriver(_host);
        }

        [Fact]
        public void GreetsYose()
        {
            _driver.GreetsYose();
            _host.Dispose();
        }

        [Fact]
        public void AnswersPing()
        {
            _driver.Pings();
            _host.Dispose();
        }
    }
}
