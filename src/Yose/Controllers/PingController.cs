using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace Yose.Controllers
{
    public class PingController
    {
        public async Task Get(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new Ping { IsAlive = true }, ToCamelCase()));
        }

        private static JsonSerializerSettings ToCamelCase()
        {
            return new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        private class Ping
        {
            public bool IsAlive;
        }
    }
}
