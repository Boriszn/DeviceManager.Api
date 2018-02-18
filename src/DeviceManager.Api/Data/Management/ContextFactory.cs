using System;
using System.Data.SqlClient;
using DeviceManager.Api.Configuration.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DeviceManager.Api.Data.Management
{
    /// <summary>
    /// Entity Framework context service
    /// (Switches the db context according to tenant id field)
    /// </summary>
    /// <seealso cref="IContextFactory"/>
    public class ContextFactory : IContextFactory
    {
        private const string TenantIdFieldName = "tenantid";
        private const string DatabaseFieldKeyword = "Database";

        private readonly HttpContext httpContext;
        private readonly IOptions<ConnectionSettings> connectionOptions;

        private readonly IDataBaseManager dataBaseManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextFactory"/> class.
        /// </summary>
        /// <param name="httpContentAccessor">The HTTP content accessor.</param>
        /// <param name="connectionOptions">The connection options.</param>
        /// <param name="dataBaseManager">The data base manager.</param>
        public ContextFactory(
            IHttpContextAccessor httpContentAccessor,
            IOptions<ConnectionSettings> connectionOptions,
            IDataBaseManager dataBaseManager)
        {
            this.httpContext = httpContentAccessor.HttpContext;
            this.connectionOptions = connectionOptions;
            this.dataBaseManager = dataBaseManager;
        }

        /// <inheritdoc />
        public IDbContext DbContext => new DeviceContext(ChangeDatabaseNameInConnectionString(this.TenantId).Options);

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
            var sqlConnectionBuilder = new SqlConnectionStringBuilder(this.connectionOptions.Value.DefaultConnection);

            // 2. Remove old Database Name from connection string
            sqlConnectionBuilder.Remove(DatabaseFieldKeyword);

            // 3. Obtain Database name from DataBaseManager and Add new DB name to 
            sqlConnectionBuilder.Add(DatabaseFieldKeyword, this.dataBaseManager.GetDataBaseName(tenantId));

            // 4. Create DbContextOptionsBuilder with new Database name
            var contextOptionsBuilder = new DbContextOptionsBuilder<DeviceContext>();

            contextOptionsBuilder.UseSqlServer(sqlConnectionBuilder.ConnectionString);

            return contextOptionsBuilder;
        }

        private void ValidateDefaultConnection()
        {
            if (string.IsNullOrEmpty(this.connectionOptions.Value.DefaultConnection))
            {
                throw new ArgumentNullException(nameof(this.connectionOptions.Value.DefaultConnection));
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
