using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DeviceManager.Api.Model;
using DeviceManager.Api.UnitTests.Api.Server;
using Newtonsoft.Json;

namespace DeviceManager.Api.UnitTests.Builders
{
    /// <summary>
    /// Fluent builder for Device API
    /// </summary>
    public class DevicesApiBuilder
    {
        private readonly TestContextFactory testContextFactory;

        private string query;
        private string deviceViewModelData;

        /// <summary>
        /// The HTTP response message
        /// </summary>
        public HttpResponseMessage HttpResponseMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesApiBuilder"/> class.
        /// </summary>
        public DevicesApiBuilder()
        {
            testContextFactory = new TestContextFactory();
        }

        public DevicesApiBuilder DefaultQuery(string version)
        {
            query = $"api/v{version}/devices";

            return this;
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

        public DevicesApiBuilder QueryWithDeviceId(string deviceId, string version)
        {
            query = $"api/v{version}/devices/{deviceId}";

            return this;
        }

        public DevicesApiBuilder QueryWithTitle(string deviceTitle, string version)
        {
            query = $"api/v{version}/devices/title/{deviceTitle}";

            return this;
        }

        /// <summary>
        /// Withs the tenant identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns></returns>
        public DevicesApiBuilder WithTenantId(string tenantId)
        {
            testContextFactory.Client.DefaultRequestHeaders.Add("tenantid", tenantId);

            return this;
        }

        public DevicesApiBuilder WithDeviceViewModelData(DeviceViewModel deviceViewModel)
        {
            deviceViewModelData = JsonConvert.SerializeObject(deviceViewModel);

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

        /// <summary>
        /// Http Get devices
        /// </summary>
        /// <returns></returns>
        public async Task<DevicesApiBuilder> Post()
        {
            // Build Post data context from json string
            var stringContent = new StringContent(
                deviceViewModelData,
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage = await testContextFactory.Client.PostAsync(query, stringContent);
            return this;
        }
    }
}
