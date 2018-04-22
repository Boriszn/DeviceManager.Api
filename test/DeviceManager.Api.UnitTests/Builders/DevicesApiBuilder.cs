using System.Net.Http;
using System.Threading.Tasks;
using DeviceManager.Api.UnitTests.Api.Server;

namespace DeviceManager.Api.UnitTests.Builders
{
    /// <summary>
    /// Fluent builder for Device API
    /// </summary>
    public class DevicesApiBuilder
    {
        private readonly TestContextFactory testContextFactory;

        private string query;
        public HttpResponseMessage HttpResponseMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesApiBuilder"/> class.
        /// </summary>
        public DevicesApiBuilder()
        {
            testContextFactory = new TestContextFactory();
        }

        /// <summary>
        /// Queries with parameters.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public DevicesApiBuilder QueryWith(int page, int pageCount, string version)
        {
            query = $"api/v{version}/devices?page={page}&pageSize={pageCount}";

            return this;
        }

        /// <summary>
        /// Withs the tenant identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns></returns>
        public DevicesApiBuilder WithTenantId(string tenantId)
        {
            testContextFactory.Client.DefaultRequestHeaders.Add("tenantid", tenantId );

            return this;
        }

        /// <summary>
        /// Http Get devices
        /// </summary>
        /// <returns></returns>
        public async Task<DevicesApiBuilder> Get()
        {
            HttpResponseMessage = await testContextFactory.Client.GetAsync(query);
            return this;
        }
    }
}
