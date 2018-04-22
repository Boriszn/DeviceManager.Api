using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DeviceManager.Api.UnitTests.Api.Server;
using DeviceManager.Api.UnitTests.Builders;
using FluentAssertions;
using Xunit;

namespace DeviceManager.Api.UnitTests.Api
{
    [Category("Integration")]
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
    }
}
