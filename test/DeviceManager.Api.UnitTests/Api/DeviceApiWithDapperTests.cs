using DeviceManager.Api.Constants;
using DeviceManager.Api.Model;
using DeviceManager.Api.UnitTests.Builders;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DeviceManager.Api.UnitTests.Api
{
    /// <summary>
    /// 
    /// </summary>
    [Trait("Category", "Integration")]
    public class DeviceApiWithDapperTests
    {
        [Fact]
        public async Task GetDevices_WithValidParameters_ReturnsOkResult()
        {
            // Arrange and Act
            var devicesApiBuilder = await new DevicesApiBuilder()
                .WithClientCredentials();

            devicesApiBuilder = await devicesApiBuilder.QueryWithDapper(page: 1, pageCount: 5, version: "1.0")
                //.WithTenantId("b0ed668d-7ef2-4a23-a333-94ad278f45d7")
                .WithTenantId(DefaultConstants.DefaultTenantGuid)
                .Get();

            // Assert
            devicesApiBuilder.HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

#if UseAuthentication

        [Fact]
        public async Task GetDevices_WithoutAuthentication_ReturnsUnauthorized()
        {
            // Arrange and Act
            var devicesApiBuilder = await new DevicesApiBuilder().QueryWithDapper(page: 1, pageCount: 5, version: "1.0")
                //.WithTenantId("b0ed668d-7ef2-4a23-a333-94ad278f45d7")
                .WithTenantId(DefaultConstants.DefaultTenantGuid)
                .Get();

            // Assert
            devicesApiBuilder.HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
#endif
        [Fact(
            Skip = "The tests will add a new item to real DB, thus should be run manually"
            )]
        public async Task PostDevice_WithDeviceModel_ReturnsOkResult()
        {
            // Arrange and Act
            var devicesApiBuilder = await new DevicesApiBuilder()
                .WithClientCredentials();

            devicesApiBuilder = await devicesApiBuilder.DefaultDapperQuery(version: "1.0")
                .WithDeviceViewModelData(new DeviceViewModel()
                {
                    DeviceCode = "DAPPER_DFGRRO12",
                    Title = "RO Controller"
                })
                .WithTenantId(DefaultConstants.DefaultTenantGuid)
                .Post();

            // Assert
            devicesApiBuilder.HttpResponseMessage.StatusCode
                .Should().Be(HttpStatusCode.OK);
        }
    }
}
