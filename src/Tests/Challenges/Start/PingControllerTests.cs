using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Tests.Extensions;
using Xunit;
using Yose.Controllers;

namespace Tests.Challenges.Start
{
    public class PingControllerTests
    {
        private PingController _controller;
        private HttpContext _context;

        public PingControllerTests()
        {
            _controller = new PingController();
            _context = new DefaultHttpContext();
            _context.Response.Body = new MemoryStream();
        }

        [Fact]
        public async Task ContentTypeIsJson()
        {
            await _controller.Get(_context);

            _context.Response.ContentType.Should().Be("application/json", "because the returned content should be JSON");
        }

        [Fact]
        public async Task ContentIsAlive()
        {
            await _controller.Get(_context);

            _context.Response.ReadAsString().Should().Be("{\"alive\":true}", "because the content should show that the server is alive");
        }
    }
}
