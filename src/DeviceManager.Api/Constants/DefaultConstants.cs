namespace DeviceManager.Api.Constants
{
    /// <summary>
    /// All hard-coded strings can be read as properties in the application
    /// </summary>
    public static class DefaultConstants
    {
        /// <summary>
        /// Name of the settings file name
        /// </summary>
        public const string AppSettingsFileName = "appsettings.json";

        /// <summary>
        /// Name of the connection string
        /// </summary>
        public const string DefaultConnection = nameof(DefaultConstants.DefaultConnection);

        /// <summary>
        /// Connection string section
        /// </summary>
        public const string ConnectionStrings = nameof(DefaultConstants.ConnectionStrings);

        /// <summary>
        /// Kerstel configuration section
        /// </summary>
        public const string Kestrel = nameof(Kestrel);

        /// <summary>
        /// Name of the AppSettings section
        /// </summary>
        public const string AppSettings = nameof(DefaultConstants.AppSettings);

        /// <summary>
        /// Name of the Authentication settings section
        /// </summary>
        public const string AuthenticationSettings = nameof(DefaultConstants.AuthenticationSettings);

        /// <summary>
        /// 
        /// </summary>
        public const string Database = nameof(DefaultConstants.Database);

        /// <summary>
        /// Address where identity server is running
        /// </summary>
        public const string AuthenticationAuthority = "AUTHENTICATION_AUTHORITY";

        /// <summary>
        /// Address of the swagger client
        /// </summary>
        public const string SwaggerClient = "SWAGGER_CLIENT";

        /// <summary>
        /// Used to display api name in the swagger UI screen
        /// </summary>
        public const string ApiDisplayName = "Device Manager Api";

        /// <summary>
        /// Used to display description of api access needed
        /// </summary>
        public const string ApiDescription = "Device Manager Api - full access";

        /// <summary>
        /// Used for passing oauth2
        /// </summary>
        public const string OAuth2 = "oauth2";

        /// <summary>
        /// Tenant id key
        /// </summary>
        public const string TenantId = "tenantid";

        /// <summary>
        /// Description for swagger tenant id field
        /// </summary>
        public const string TenantIdSwaggerDescription = "Tenant Id, Type: GUID (e.g b0ed668d-7ef2-4a23-a333-94ad278f45d7)";

        /// <summary>
        /// Parameter location
        /// </summary>
        public const string Header = "header";
        
        /// <summary>
        /// Name of the device database tenant 1
        /// </summary>
        public const string DefaultTeanantDatabase = "DeviceDb";

        /// <summary>
        /// Name of the device database tenant 2
        /// </summary>
        public const string DeviceDbTenant2 = "DeviceDb-ten2";

        /// <summary>
        /// Guid of the first tenant
        /// </summary>
        public const string DefaultTenantGuid = "b0ed668d-7ef2-4a23-a333-94ad278f45d7";

        /// <summary>
        /// Guid of the first tenant
        /// </summary>
        public const string Tenant2Guid = "e7e73238-662f-4da2-b3a5-89f4abb87969";

        /// <summary>
        /// Name of the logging section in the config files
        /// </summary>
        public const string Logging = nameof(DefaultConstants.Logging);

        /// <summary>
        /// Resource Test item
        /// </summary>
        public const string Hello = nameof(DefaultConstants.Hello);

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

    }
}
