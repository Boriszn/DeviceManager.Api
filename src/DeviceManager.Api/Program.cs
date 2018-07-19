using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DeviceManager.Api
{
    /// <summary>
    /// Starting or hosting class of the applcation 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry method for dotnet core hosting
        /// </summary>
        /// <param name="args">Command line parameters</param>
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
