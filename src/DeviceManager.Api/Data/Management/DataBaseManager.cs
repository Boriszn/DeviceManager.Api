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
        /// IMPORTANT NOTICE: The solution uses simple dictionary for demo purposes.
        /// The Best "Real-life" solutions would be creating 'RootDataBase' with 
        /// all Tenants Parameters/Options like: TenantName, DatabaseName, other configuration.
        /// </summary>
        private readonly Dictionary<Guid, string> tenantConfigurationDictionary = new Dictionary<Guid, string>
        {
            {
                Guid.Parse(Constants.Tenant1Guid), Constants.DeviceDb
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