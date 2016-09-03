using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace Yose
{
    public class Server : CommandLineApplication
    {
        private const string DefaultEnvironment = "dev";

        private Player _player;
        private CommandOption _environment;

        public Server()
        {
            _player = new Player();
            _environment = Option("-e | --environment", "Environment to use (dev or prod)", CommandOptionType.SingleValue);

            HelpOption("-h | --help");
            VersionOption("-v | --version", typeof(Server).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);

            OnExecute(() => Launch());
        }

        private string GetConfigurationFile()
        {
            var environment = _environment.HasValue() ? _environment.Value() : DefaultEnvironment;

            return Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Configurations" + Path.DirectorySeparatorChar + environment + ".json";
        }

        private int Launch()
        {
            _player.Start(new ConfigurationBuilder().AddJsonFile(GetConfigurationFile(), false).Build());

            Console.WriteLine("Yose playser server started on {0}", _player.Uri());
            Console.Read();
            Console.WriteLine("Yose playser server stopping");

            _player.Stop();

            return 0;
        }

        public static void Main(string[] args)
        {
            new Server().Execute(args);
        }
    }
}
