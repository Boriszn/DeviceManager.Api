using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceManager.Api.Configuration
{
    /// <summary>
    /// IOC contaner start-up configuration
    /// </summary>
    public static class IocContainerConfiguration
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureService(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IDeviceService, DeviceService>();

            services.AddTransient<IDataBaseManager, DataBaseManager>();
            services.AddTransient<IContextFactory, ContextFactory>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}