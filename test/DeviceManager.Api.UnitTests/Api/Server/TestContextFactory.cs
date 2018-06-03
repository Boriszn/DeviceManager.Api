using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace DeviceManager.Api.UnitTests.Api.Server
{
    /// <summary>
    /// Create Hosting Test server and instantiate Test context 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class TestContextFactory : IDisposable
    {
        private TestServer server;
        public HttpClient Client { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestContextFactory"/> class.
        /// </summary>
        public TestContextFactory()
        {
            SetUpClient();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        }

        /// <summary>
        /// Sets up client.
        /// </summary>
        private void SetUpClient()
        {
            server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            Client = server.CreateClient();
        }
    }
}
