using DeviceManager.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceManager.Api.Configuration
{
    public static class EntityFrameworkConfiguration
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

            services.AddScoped<IDbContext, DeviceContext>();
        }
    }
}