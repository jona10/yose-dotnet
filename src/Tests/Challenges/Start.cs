using Tests.Drivers;
using Xunit;
using Yose;

namespace Tests.Challenges
{
    public class Start
    {
        private PlayerDriver _driver;
        private Player _player;

        public Start()
        {
            _player = new Player();
            _driver = new PlayerDriver(_player);
        }

        [Fact]
        public void GreetsYose()
        {
            _driver.GreetsYose();
            _player.Stop();
        }

        [Fact]
        public void AnswersPing()
        {
            _driver.Pings();
            _player.Stop();
        }
    }
}
