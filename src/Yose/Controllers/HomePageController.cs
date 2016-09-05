using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;

namespace Yose.Controllers
{
    public class HomePageController
    {
        private IHostingEnvironment _environment;

        public HomePageController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task Get(HttpContext context)
        {
            var viewPath = Path.Combine(_environment.ContentRootPath, "Views", "home.html");
            await context.Response.WriteAsync(File.ReadAllText(viewPath, Encoding.UTF8));
        }
    }
}
