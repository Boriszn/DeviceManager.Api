using System;

namespace DeviceManager.Api.Helpers
{
    /// <summary>
    /// All hardcoded strings can be read as properties in the application
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Name of the connection string
        /// </summary>
        public const string DefaultConnection = nameof(DefaultConnection);

        /// <summary>
        /// Connection string section
        /// </summary>
        public const string ConnectionStrings = nameof(ConnectionStrings);

        /// <summary>
        /// Name of the settings section
        /// </summary>
        public const string AppSettings = nameof(AppSettings);

        /// <summary>
        /// 
        /// </summary>
        public const string Database = nameof(Database);

        /// <summary>
        /// 
        /// </summary>
        public const string TenantId = "tenantid";

        /// <summary>
        /// Parameter location
        /// </summary>
        public const string Header = "header";
        
        /// <summary>
        /// Name of the device database tenant 1
        /// </summary>
        public const string DeviceDb = nameof(DeviceDb);

        /// <summary>
        /// Name of the device database tenant 2
        /// </summary>
        public const string DeviceDbTenant2 = "DeviceDb-ten2";

        /// <summary>
        /// Guid of the first tenant
        /// </summary>
        public const string Tenant1Guid = "b0ed668d-7ef2-4a23-a333-94ad278f45d7";

        /// <summary>
        /// Guid of the first tenant
        /// </summary>
        public const string Tenant2Guid = "e7e73238-662f-4da2-b3a5-89f4abb87969";

        /// <summary>
        /// Name of the logging section in the config files
        /// </summary>
        public const string Logging = nameof(Logging);

        /// <summary>
        /// 
        /// </summary>
        public const string Hello = nameof(Hello);

        /// <summary>
        /// Name of the ui culture property in the header
        /// </summary>
        public const string UiCulture = "ui-culture";

        /// <summary>
        /// Name of the culture property in the header
        /// </summary>
        public const string Culture = "culture";

        /// <summary>
        /// Name of the culture property in the header
        /// </summary>
        public const string Query = "query";

        /// <summary>
        /// Name of the culture property in the header
        /// </summary>
        public const string EnglishCulture = "en-US";

        /// <summary>
        /// Name of the culture property in the header
        /// </summary>
        public const string StringInText = "string";

        #region Data seeding

        /// <summary>
        /// Guid of the device group
        /// </summary>
        public static readonly Guid SeedDeviceGroupId = new Guid("843a92af-9174-49a3-a2e7-08f99919d6ca");

        /// <summary>
        /// Company name
        /// </summary>
        public const string SeedDeviceGroupCompany = "Microsoft";

        /// <summary>
        /// Operating System
        /// </summary>
        public const string SeedDeviceGroupOS = "Windows 10";

        #region Seed device 1
        
        /// <summary>
        /// Device code
        /// </summary>
        public static readonly Guid SeedDevice1Id = new Guid("9b34ae90-f226-43df-8ad0-7cfdce2f16a7");

        /// <summary>
        /// Device code
        /// </summary>
        public const string SeedDevice1Title = "X Box";

        /// <summary>
        /// Device code
        /// </summary>
        public const string SeedDevice1Code = "Xbox1234";

        #endregion Seed device 1

        #region Seed device 2

        /// <summary>
        /// Second seed device Guid
        /// </summary>
        public static readonly Guid SeedDevice2Id = new Guid("1ee62dd5-d698-4e67-a260-f5a66f86f0df");

        /// <summary>
        /// Device title
        /// </summary>
        public const string SeedDevice2Title = "Surface Tablet";

        /// <summary>
        /// Device code
        /// </summary>
        public const string SeedDevice2Code = "Surface568";

        #endregion Seed device 2

        #endregion Data seeding
    }
}
