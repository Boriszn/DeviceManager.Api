namespace DeviceManager.IdentityServer.Helpers
{
    /// <summary>
    /// Define all application related constants here
    /// </summary>
    public static class ApplicationConstants
    {
        /// <summary>
        /// claim name to store tenant id in the database
        /// </summary>
        public const string TenantClaim = "tenant_id";

        /// <summary>
        /// Name of the environment variable containing swagger client name
        /// </summary>
        public const string SwaggerClient = "SWAGGER_CLIENT";

        /// <summary>
        /// Name of the API
        /// </summary>
        public const string DeviceManagerApi = "DeviceManagerApi";

        /// <summary>
        /// Name of the test client
        /// </summary>
        public const string DeviceManagerTestClient = "DeviceManagerApi_UnitTest";

        /// <summary>
        /// Name of the swagger client
        /// </summary>
        public const string DeviceManagerSwaggerClient =  "DeviceManagerApi_Swagger";

        /// <summary>
        /// Secret key for test client to access
        /// </summary>
        public const string DeviceManagerTestClientSecret = "3c8f10d5-db6f-4620-ad75-63b31cadc071";

    }
}
