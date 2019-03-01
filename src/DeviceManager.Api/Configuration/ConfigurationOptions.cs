using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceManager.Api.Configuration
{
    /// <summary>
    /// Registers all the settings from configuration file in the IOC container
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
            services.Configure<ConnectionSettings>(configuration.GetSection(DefaultConstants.ConnectionStrings));
            services.Configure<AppSettings>(configuration.GetSection(DefaultConstants.AppSettings));
        }
    }
}