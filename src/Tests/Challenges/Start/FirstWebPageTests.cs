using System;
using Tests.Drivers;
using Xunit;

namespace Tests.Challenges.Start
{
    public class FirstWebPageTests : IDisposable
    {
        private PlayerDriver _player;
        private Server _server;

        public FirstWebPageTests()
        {
            _server = new Server();
            _player = new PlayerDriver(_server);
        }

        public void Dispose()
        {
            _server.Stop();
        }

        [Fact]
        public void GreetsYose()
        {
            _player.GreetsYose();
        }
    }
}
