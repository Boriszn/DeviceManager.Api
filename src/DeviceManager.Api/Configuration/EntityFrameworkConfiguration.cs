using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Data;
using DeviceManager.Api.Helpers;
using Microsoft.EntityFrameworkCore;
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
        public static void ConfigureService(IServiceCollection services, IConfigurationRoot configuration)
        {
            string connectionString = configuration.GetConnectionString(Constants.DefaultConnection);

            // Database connection settings
            var connectionOptions = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionSettings>>();

            // Entity framework configuration
            services.AddEntityFrameworkNpgsql().AddDbContext<DeviceContext>(options =>
                GetContextBuilder(options, connectionOptions, connectionString));

            services.AddScoped<IDbContext, DeviceContext>();
        }

        /// <summary>
        /// Based on the database context builder instance is created
        /// </summary>
        /// <param name="optionsBuilder">Context builder</param>
        /// <param name="connectionOptions">Configured connection settings</param>
        /// <param name="connectionString">Configured connection string</param>
        /// <returns>Context builder with configured database settings</returns>
        private static DbContextOptionsBuilder GetContextBuilder(DbContextOptionsBuilder optionsBuilder, IOptions<ConnectionSettings> connectionOptions, string connectionString)
        =>
            connectionOptions.Value.UseNoSql ? optionsBuilder.UseNpgsql(connectionString, b => GetMigrationInformation(b)) : optionsBuilder.UseSqlServer(connectionString, b => GetMigrationInformation(b));

        /// <summary>
        ///  Configures the assembly where migrations are maintained for this context.
        ///  <see href="https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/migrations/index" /> for migrations
        ///  <see href="https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet"/> for command line tools
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <typeparam name="TExtension"></typeparam>
        /// <param name="builder"></param>
        /// <returns>Migrations configured builder</returns>
        private static TBuilder GetMigrationInformation<TBuilder, TExtension>(RelationalDbContextOptionsBuilder<TBuilder, TExtension> builder)
             where TBuilder : RelationalDbContextOptionsBuilder<TBuilder, TExtension>
            where TExtension : RelationalOptionsExtension, new()
        {
            
            return builder.MigrationsAssembly(typeof(DeviceContext).Assembly.GetName().Name);
        }
    }
}