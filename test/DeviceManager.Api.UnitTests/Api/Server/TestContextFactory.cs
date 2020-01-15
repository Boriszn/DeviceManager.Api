using DeviceManager.Api.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;

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
            var server = new TestServer(new WebHostBuilder().UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment("Development")
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // Moved configuration here because Kestel was not able to access the configuration settings
                    Environment.SetEnvironmentVariable("AUTHENTICATION_AUTHORITY", "http://devicemanager.identityserver:5000/");
                    config.AddJsonFile(DefaultConstants.AppSettingsFileName, optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

                }).UseStartup<Startup>());


            Client = server.CreateClient();
        }
    }
}
