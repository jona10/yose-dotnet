using Microsoft.Extensions.Configuration;
using System;

namespace Yose
{
    public class Server
    {
        private Player _player;
        private IConfiguration _configuration;

        public Server(string[] args)
        {
            _configuration = new ConfigurationBuilder().AddCommandLine(args).Build();
            _player = new Player();
        }

        private void Launch()
        {
            _player.Run(_configuration);
        }

        public static void Main(string[] args)
        {
            new Server(args).Launch();
        }
    }
}
