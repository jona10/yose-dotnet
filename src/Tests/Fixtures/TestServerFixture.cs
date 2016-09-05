using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace Tests.Fixtures
{
    public class TestServerFixture<TStartup> : IDisposable
    {
        private const string SolutionName = "Yose.sln";
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TestServerFixture() : this(Path.Combine("src"))
        {
        }

        protected TestServerFixture(string solutionRelativeTargetProjectParentDir)
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            var contentRoot = GetProjectPath(solutionRelativeTargetProjectParentDir, startupAssembly);

            var builder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .UseEnvironment("Development")
                .UseStartup(typeof(TStartup));

            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }

        public HttpClient Client
        {
            get { return _client; }
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        private static string GetProjectPath(string solutionRelativePath, Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name;
            var directoryInfo = new DirectoryInfo(PlatformServices.Default.Application.ApplicationBasePath);

            return GetProjectPath(directoryInfo, solutionRelativePath, projectName);
        }

        private static string GetProjectPath(DirectoryInfo directoryInfo, string solutionRelativePath, string projectName)
        {
            var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, SolutionName));
            if (solutionFileInfo.Exists)
            {
                return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
            }

            if (directoryInfo.Parent == null)
            {
                throw new Exception($"Solution root could not be located using application root {directoryInfo.Name}.");
            }

            return GetProjectPath(directoryInfo.Parent, solutionRelativePath, projectName);
        }
    }
}
