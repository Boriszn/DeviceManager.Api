using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceManager.Api.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConfigurationOptions
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureService(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<ConnectionSettings>(configuration.GetSection(Constants.ConnectionStrings));
        }
    }
}