using System;
using System.Linq;
using DeviceManager.Api.Configuration.DatabaseTypes;
using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Constants;
using DeviceManager.Api.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DeviceManager.Api.Configuration
{
    /// <summary>
    /// Configurations related to entity framework
    /// </summary>
    public static class EntityFrameworkConfiguration
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(DefaultConstants.DefaultConnection);

            // Database connection settings
            var connectionOptions = services.BuildServiceProvider().GetRequiredService<ConnectionSettings>();

            RegisterDatabaseType(services, connectionOptions);

            var databaseTypeInstance = services.BuildServiceProvider().GetRequiredService<IDatabaseType>();

            databaseTypeInstance.EnableDatabase(services, connectionOptions);

            // Entity framework configuration
            services.AddDbContext<DeviceContext>(options => 
                databaseTypeInstance.GetContextBuilder(options, connectionOptions, connectionString));

            services.AddScoped<IDbContext, DeviceContext>();
        }

        /// <summary>
        ///  Configures the assembly where migrations are maintained for this context.
        ///  <see href="https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/migrations/index" /> for migrations
        ///  <see href="https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet"/> for command line tools
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <typeparam name="TExtension"></typeparam>
        /// <param name="builder"></param>
        /// <returns>Migrations configured builder</returns>
        public static TBuilder GetMigrationInformation<TBuilder, TExtension>(RelationalDbContextOptionsBuilder<TBuilder, TExtension> builder)
             where TBuilder : RelationalDbContextOptionsBuilder<TBuilder, TExtension>
            where TExtension : RelationalOptionsExtension, new()
        {

            return builder.MigrationsAssembly(typeof(DeviceContext).Assembly.GetName().Name);
        }

        /// <summary>
        /// Inject database settings instance based on the connection string
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionOptions"></param>
        private static void RegisterDatabaseType(IServiceCollection services, ConnectionSettings connectionOptions)
        {
            var databaseInterfaceType = typeof(IDatabaseType);
            var instanceType = connectionOptions.DatabaseType.ToString();
            var instance = databaseInterfaceType.Assembly.GetTypes().FirstOrDefault(x =>
             databaseInterfaceType.IsAssignableFrom(x)
             &&
             string.Equals(instanceType, x.Name, StringComparison.OrdinalIgnoreCase));
            services.AddSingleton<IDatabaseType>((IDatabaseType)Activator.CreateInstance(instance));
        }

    }
}