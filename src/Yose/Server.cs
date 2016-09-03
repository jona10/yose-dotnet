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
            _player.Start(_configuration);

            Console.WriteLine("Yose player server started on {0}", _player.Uri());
            Console.Read();
            Console.WriteLine("Yose player server stopping");

            _player.Stop();
        }

        public static void Main(string[] args)
        {
            new Server(args).Launch();
        }
    }
}
