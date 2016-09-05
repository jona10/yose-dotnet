using Tests.Drivers;
using Tests.Fixtures;
using Xunit;
using Yose;

namespace Tests.Challenges
{
    public class StartTests : IClassFixture<TestServerFixture<Startup>>
    {
        private const string SolutionName = "Yose.sln";
        private YoseDriver _driver;

        public StartTests(TestServerFixture<Startup> server)
        {
            _driver = new YoseDriver(server.Client);
        }

        [Fact]
        public void Greets()
        {
            _driver.Home().GreetsWith("Hello Yose");
        }

        [Fact]
        public void IsAlive()
        {
            _driver.Api().IsAlive();
        }
    }
}
