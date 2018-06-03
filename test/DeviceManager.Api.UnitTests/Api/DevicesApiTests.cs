using System.Net;
using System.Threading.Tasks;
using DeviceManager.Api.Model;
using DeviceManager.Api.UnitTests.Builders;
using FluentAssertions;
using Xunit;

namespace DeviceManager.Api.UnitTests.Api
{
    /// <summary>
    /// Intergration tests for Device API controller
    /// </summary>
    [Trait("Category", "Integration")]
    public class DevicesApiTests
    {
        [Fact]
        public async Task GetDevices_WithValidParameters_ReturnsOkResult()
        {
            // Arrange and Act
            var devicesApiBuilder = await new DevicesApiBuilder()
                .QueryWith(page: 1, pageCount: 5, version:"1.0")
                .WithTenantId("b0ed668d-7ef2-4a23-a333-94ad278f45d7")
                .Get();

            // Assert
            devicesApiBuilder.HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetDevices_WithoutTenantId_ReturnsInternalServerErrorResult()
        {
            // Arrange and Act
            var devicesApiBuilder = await new DevicesApiBuilder()
                .QueryWith(page: 1, pageCount: 5, version: "1.0")
                .Get();

            // Assert
            devicesApiBuilder.HttpResponseMessage.StatusCode
                .Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task GetDeviceById_WithTenantId_ReturnsOkResult()
        {
            // Arrange and Act
            var devicesApiBuilder = await new DevicesApiBuilder()
                .QueryWithDeviceId("12B89FFB-A053-47E5-BAE1-81644DCEA79F", version: "1.0")
                .WithTenantId("e7e73238-662f-4da2-b3a5-89f4abb87969")
                .Get();

            // Assert
            devicesApiBuilder.HttpResponseMessage.StatusCode
                .Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetDeviceByTitle_WithTenantId_ReturnsOkResult()
        {
            // Arrange and Act
            var devicesApiBuilder = await new DevicesApiBuilder()
                .QueryWithTitle("RF123GH", version: "1.0")
                .WithTenantId("e7e73238-662f-4da2-b3a5-89f4abb87969")
                .Get();

            // Assert
            devicesApiBuilder.HttpResponseMessage.StatusCode
                .Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact ( 
            Skip = "The tests will add a new item to real DB, thus should be run manualy"
            )]
        public async Task PostDevice_WithDeviceModel_ReturnsOkResult()
        {
            // Arrange and Act
            var devicesApiBuilder = await new DevicesApiBuilder()
                .DefaultQuery(version: "1.0")
                .WithDeviceViewModelData(new DeviceViewModel()
                {
                    DeviceCode = "DFGRRO12", Title = "RO Controller"
                })
                .WithTenantId("e7e73238-662f-4da2-b3a5-89f4abb87969")
                .Post();

            // Assert
            devicesApiBuilder.HttpResponseMessage.StatusCode
                .Should().Be(HttpStatusCode.OK);
        }
    }
}
