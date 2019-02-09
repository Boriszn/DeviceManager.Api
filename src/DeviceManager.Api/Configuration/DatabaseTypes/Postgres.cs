using System.Data.Common;
using DeviceManager.Api.Configuration.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DeviceManager.Api.Configuration.DatabaseTypes
{
    /// <inherit/>
    public class Postgres : IDatabaseType
    {
        /// <inherit/>
        public IServiceCollection EnableDatabase(IServiceCollection services, ConnectionSettings connectionOptions)
        {
            services.AddEntityFrameworkNpgsql();
            return services;
        }

        /// <inherit/>
        public DbConnectionStringBuilder GetConnectionBuilder(string connectionString)
        {
            return new Npgsql.NpgsqlConnectionStringBuilder(connectionString);
        }

        /// <inherit/>
        public DbContextOptionsBuilder GetContextBuilder(DbContextOptionsBuilder optionsBuilder, ConnectionSettings connectionOptions, string connectionString)
        {
            return optionsBuilder.UseNpgsql(connectionString, b => EntityFrameworkConfiguration.GetMigrationInformation(b));
        }

        /// <inherit/>
        public DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(DbContextOptionsBuilder<TContext> contextOptionsBuilder, string connectionString) where TContext : DbContext
        {
            return contextOptionsBuilder.UseNpgsql(connectionString);
        }
    }
}