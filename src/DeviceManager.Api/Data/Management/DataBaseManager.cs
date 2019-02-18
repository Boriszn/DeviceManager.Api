using DeviceManager.Api.Helpers;
using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Data.Management
{
    /// <summary>
    /// Contains all tenants database mappings and options
    /// </summary>
    public class DataBaseManager : IDataBaseManager
    {
        /// <summary>
        /// IMPORTANT NOTICE: Tenant Configuration was implemented as Dictionary for demo purposes only 
        /// In a production application I would recommend following options:
        /// - create SQL root database or table
        /// - create NoSql root database/collection
        /// - move the dictionary the Redis cache  
        /// </summary>
        private readonly Dictionary<Guid, string> tenantConfigurationDictionary = new Dictionary<Guid, string>
        {
            {
                Guid.Parse(Constants.DefaultTenantGuid), Constants.DefaultTeanantDatabase
            },
            {
                Guid.Parse(Constants.Tenant2Guid), Constants.DeviceDbTenant2
            }
        };

        /// <summary>
        /// Gets the name of the data base.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>db name</returns>
        public string GetDataBaseName(string tenantId)
        {
            var dataBaseName = this.tenantConfigurationDictionary[Guid.Parse(tenantId)];

            if (dataBaseName == null)
            {
                throw new ArgumentNullException(nameof(dataBaseName));
            }

            return dataBaseName;
        }
    }
}