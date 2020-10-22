using DeviceManager.Api.Configuration.DatabaseTypes;
using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Constants;
using DeviceManager.Api.Data.DataSeed;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace DeviceManager.Api.Data.Management
{
    /// <summary>
    /// Entity Framework context service
    /// (Switches the db context according to tenant id field)
    /// </summary>
    /// <seealso cref="IContextFactory"/>
    public class ContextFactory : IContextFactory
    {
        private const string TenantIdFieldName = DefaultConstants.TenantId;
        private const string DatabaseFieldKeyword = DefaultConstants.Database;
        private readonly HttpContext httpContext;

        private readonly ConnectionSettings connectionOptions;

        private readonly IDataBaseManager dataBaseManager;
        private readonly IDatabaseType databaseType;
        private readonly IDataSeeder dataSeeder;
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextFactory"/> class.
        /// </summary>
        /// <param name="httpContentAccessor">The HTTP content accessor.</param>
        /// <param name="connectionOptions">The connection options.</param>
        /// <param name="dataBaseManager">The data base manager.</param>
        /// <param name="databaseType">Type of the database</param>
        /// <param name="dataSeeder">Data seeder</param>
        /// <param name="loggerFactory"></param>
        public ContextFactory(IHttpContextAccessor httpContentAccessor,
            ConnectionSettings connectionOptions,
            IDataBaseManager dataBaseManager,
            IDatabaseType databaseType, IDataSeeder dataSeeder,
            ILoggerFactory loggerFactory)
        {
            this.httpContext = httpContentAccessor.HttpContext;
            this.connectionOptions = connectionOptions;
            this.dataBaseManager = dataBaseManager;
            this.databaseType = databaseType;
            this.dataSeeder = dataSeeder;
            this.loggerFactory = loggerFactory;
        }

        /// <inheritdoc />
        public IDbContext DbContext => new DeviceContext(
            ChangeDatabaseNameInConnectionString(this.TenantId).Options,
            this.dataSeeder,
            this.loggerFactory);

        /// <summary>
        /// Gets tenant id from HTTP header
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        /// <exception cref="ArgumentNullException">
        /// httpContext
        /// or
        /// tenantId
        /// </exception>
        private string TenantId
        {
            get
            {
                ValidateHttpContext();

                string tenantId = this.httpContext.Request.Headers[TenantIdFieldName].ToString();

                ValidateTenantId(tenantId);

                return tenantId;
            }
        }

        private DbContextOptionsBuilder<DeviceContext> ChangeDatabaseNameInConnectionString(string tenantId)
        {
            ValidateDefaultConnection();

            // 1. Create Connection String Builder using Default connection string
            var connectionBuilder = databaseType.GetConnectionBuilder(connectionOptions.DefaultConnection);

            // 2. Remove old Database Name from connection string
            connectionBuilder.Remove(DatabaseFieldKeyword);

            // 3. Obtain Database name from DataBaseManager and Add new DB name to 
            connectionBuilder.Add(DatabaseFieldKeyword, this.dataBaseManager.GetDataBaseName(tenantId));

            // 4. Create DbContextOptionsBuilder with new Database name
            var contextOptionsBuilder = new DbContextOptionsBuilder<DeviceContext>();

            databaseType.SetConnectionString(contextOptionsBuilder, connectionBuilder.ConnectionString);

            return contextOptionsBuilder;
        }

        private void ValidateDefaultConnection()
        {
            if (string.IsNullOrEmpty(this.connectionOptions.DefaultConnection))
            {
                throw new ArgumentNullException(nameof(this.connectionOptions.DefaultConnection));
            }
        }

        private void ValidateHttpContext()
        {
            if (this.httpContext == null)
            {
                throw new ArgumentNullException(nameof(this.httpContext));
            }
        }

        private static void ValidateTenantId(string tenantId)
        {
            if (tenantId == null)
            {
                throw new ArgumentNullException(nameof(tenantId));
            }

            if (!Guid.TryParse(tenantId, out Guid tenantGuid))
            {
                throw new ArgumentNullException(nameof(tenantId));
            }
        }
    }
}
