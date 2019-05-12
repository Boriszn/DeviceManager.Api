using DeviceManager.Api.ActionFilters.Settings;
using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Helpers;
using Microsoft.AspNetCore.Hosting;
using DeviceManager.Api.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IStartupFilter, SettingValidationStartupFilter>();

            services.Configure<ConnectionSettings>(configuration.GetSection(DefaultConstants.ConnectionStrings));
            services.Configure<AppSettings>(configuration.GetSection(DefaultConstants.AppSettings));

            // Explicitly register the settings object so IOptions not required (optional)
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ConnectionSettings>>().Value);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AppSettings>>().Value);

            // Register as an IValidatable
            services.AddSingleton<IValidatable>(resolver =>
                resolver.GetRequiredService<IOptions<ConnectionSettings>>().Value);
            services.AddSingleton<IValidatable>(resolver =>
                resolver.GetRequiredService<IOptions<AppSettings>>().Value);
        }
    }
}