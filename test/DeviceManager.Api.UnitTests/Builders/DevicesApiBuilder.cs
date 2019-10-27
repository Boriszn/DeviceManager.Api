using DeviceManager.Api.Constants;
using DeviceManager.Api.Helpers;
using DeviceManager.Api.Model;
using DeviceManager.Api.UnitTests.Api.Server;
using DeviceManager.Api.UnitTests.Constants;
using IdentityModel.Client;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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

        public DevicesApiBuilder DefaultDapperQuery(string version)
        {
            query = $"api/v{version}/devices/dapper";

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

        public DevicesApiBuilder QueryWithDapper(int page, int pageCount, string version)
        {
            query = $"api/v{version}/devices/dapper?page={page}&pageSize={pageCount}";

            return this;
        }

        public DevicesApiBuilder QueryWithDeviceId(string deviceId, string version)
        {
            query = $"api/v{version}/devices/{deviceId}";

            return this;
        }

        public DevicesApiBuilder QueryWithDeviceIdAsync(string deviceId, string version)
        {
            query = $"api/v{version}/devices/async/{deviceId}";

            return this;
        }

        public DevicesApiBuilder QueryWithTitle(string deviceTitle, string version)
        {
            query = $"api/v{version}/devices/title/{deviceTitle}";

            return this;
        }

        public async Task<DevicesApiBuilder> WithClientCredentials()
        {
#if UseAuthentication
            using (var identityClient = new HttpClient())
            {

                var discoveryDocument = new DiscoveryDocumentRequest
                {
                    Address = GenericHelper.GetUriFromEnvironmentVariable(DefaultConstants.AuthenticationAuthority).ToString()
                };
                discoveryDocument.Policy.RequireHttps = false;
                var discoveryResponse = await identityClient.GetDiscoveryDocumentAsync(discoveryDocument);
                if (discoveryResponse.IsError)
                {
                    Assert.True(false, discoveryResponse.Error);
                    return this;
                }


                // request token
                var tokenResponse = await identityClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = discoveryResponse.TokenEndpoint,
                    ClientId = DefaultConstants.DeviceManagerTestClient,
                    ClientSecret = TestConstants.DeviceManagerTestClientSecret,
                    Scope = DefaultConstants.ApiName
                });
                if (tokenResponse.IsError)
                {
                    Assert.True(false, tokenResponse.Error);
                    return this;
                }

                testContextFactory.Client.SetBearerToken(tokenResponse.AccessToken);

            }
#endif
            return this;
        }

        /// <summary>
        /// Withs the tenant identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns></returns>
        public DevicesApiBuilder WithTenantId(string tenantId)
        {
            testContextFactory.Client.DefaultRequestHeaders.Add(DefaultConstants.TenantId, tenantId);

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

        public async Task<DevicesApiBuilder> PostUsingDapper()
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
