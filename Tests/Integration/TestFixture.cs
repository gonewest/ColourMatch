using Colours;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace Tests.Controller
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _server;
        private Type _tstart = typeof(Startup);

        public HttpClient Client { get; }

        public TestFixture()
            : this(Path.Combine("Colours"))
        {
        }

        protected TestFixture(string relativeTargetProjectParentDir)
        {
            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();
            _server = new TestServer(builder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost");
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
