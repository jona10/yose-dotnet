using Tests.Drivers;
using Tests.Fixtures;
using Xunit;
using Yose;

namespace Tests.Challenges
{
    public class StartTests : IClassFixture<TestServerFixture<Startup>>
    {
        private const string SolutionName = "Yose.sln";
        private PlayerDriver _driver;

        public StartTests(TestServerFixture<Startup> server)
        {
            _driver = new PlayerDriver(server.Client);
        }

        [Fact]
        public void GreetsYose()
        {
            _driver.GreetsYose();
        }

        [Fact]
        public void AnswersPing()
        {
            _driver.Pings();
        }
    }
}
