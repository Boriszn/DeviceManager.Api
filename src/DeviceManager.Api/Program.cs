using DeviceManager.Api.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DeviceManager.Api
{
    /// <summary>
    /// Starting or hosting class of the applcation 
    /// To change the Environment change the ASPNETCORE_ENVIRONMENT value of the profile in launchSettings.json file.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry method for dotnet core hosting
        /// </summary>
        /// <param name="args">Command line parameters</param>
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.Run();
        }
        /// <summary>
        /// Fix migrations https://github.com/aspnet/EntityFrameworkCore/issues/9415#issuecomment-327589912
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                // Moved configuration here because Kestel was not able to access the configuration settings

                config.AddJsonFile(DefaultConstants.AppSettingsFileName, optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            })
            .UseStartup<Startup>()
            // Currently Kestrel uses HTTPS in Production environment
            // To know more about Kestrel configuration visit  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel
            .UseKestrel((context, options) => options.Configure(context.Configuration.GetSection(DefaultConstants.Kestrel)))
            .UseApplicationInsights().Build();
    }
}
