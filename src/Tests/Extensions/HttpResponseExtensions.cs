using Microsoft.AspNetCore.Http;
using System.IO;

namespace Tests.Extensions
{
    public static class HttpResponseExtensions
    {
        public static string ReadAsString(this HttpResponse response)
        {
            var reader = new StreamReader(response.Body);
            response.Body.Seek(0, SeekOrigin.Begin);

            return reader.ReadToEnd();
        }
    }
}
