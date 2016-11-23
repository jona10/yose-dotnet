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
        private readonly PingController _controller;
        private readonly HttpContext _context;

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

            _context.Response.ContentType.Should().Be("application/json");
        }

        [Fact]
        public async Task ContentIsAlive()
        {
            await _controller.Get(_context);

            _context.Response.ReadAsString().Should().Be("{\"alive\":true}");
        }
    }
}
