using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Linq;

namespace Tests.Extensions
{
    public static class WebHostExtensions
    {
        public static string Uri(this IWebHost host)
        {
            return ((IServerAddressesFeature)host.ServerFeatures[typeof(IServerAddressesFeature)]).Addresses.First();
        }
    }
}
