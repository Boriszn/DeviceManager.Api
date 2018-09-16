using System.Data.Common;
using DeviceManager.Api.Configuration.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DeviceManager.Api.Configuration.DatabaseTypes
{
    /// <summary>
    /// All database connection types should implement this interface to connect via entity framework core.
    /// </summary>
    public interface IDatabaseType
    {
        /// <summary>
        /// Enables database type in the service collection. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionOptions"></param>
        /// <returns></returns>
        IServiceCollection EnableDatabase(IServiceCollection services, IOptions<ConnectionSettings> connectionOptions);

        /// <summary>
        /// Based on the database context builder instance is created
        /// </summary>
        /// <param name="optionsBuilder">Context builder</param>
        /// <param name="connectionOptions">Configured connection settings</param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        DbContextOptionsBuilder GetContextBuilder(DbContextOptionsBuilder optionsBuilder, IOptions<ConnectionSettings> connectionOptions, string connectionString);

        /// <summary>
        /// Based on the database type and tenant id connection object is built
        /// </summary>
        /// <param name="connectionString">New connection string</param>
        /// <returns></returns>
        DbConnectionStringBuilder GetConnectionBuilder(string connectionString);

        /// <summary>
        /// updates with new connection string
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="contextOptionsBuilder"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(DbContextOptionsBuilder<TContext> contextOptionsBuilder, string connectionString) where TContext : DbContext;
    }
}