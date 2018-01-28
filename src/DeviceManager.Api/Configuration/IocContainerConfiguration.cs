using DeviceManager.Api.Data;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Services;
using Microsoft.EntityFrameworkCore;
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
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Entity framework configuration
            services.AddDbContext<DeviceContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}