using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Model;
using DeviceManager.Api.Services;
using DeviceManager.Api.UnitTests.Builders;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DeviceManager.Api.UnitTests.Services
{
    public class DeviceServiceWithDapperTests : IDisposable
    {
        private readonly IDeviceService service;

        /// <summary>
        /// Test device services using dapper
        /// </summary>
        public DeviceServiceWithDapperTests()
        {
            // Build Device 
            Device device = new DeviceBuilder()
                .WithId("27be25a2-1b69-4476-a90f-f80498f5e2ec")
                .WithTitle("Raspberry3")
                .Build();

            // Build Device list
            List<Device> devicesList = new List<Device> { device };


            // Build device service with dapper UOW
            service = new DeviceServiceBuilder()
                .WithDapperRepositoryMock(devicesList, device)
                .WithValidationMock()
                .WithDapperUnitOfWorkSetup()
                .Build();
        }

        public void Dispose()
        {
            // TODO: Correct verification 
            // this.mockRepository.VerifyAll();
        }
        
        [Fact]
        public void CreateDevice_WithValidParameters_SholdNotThrowAnyExceptions()
        {
            // Arrange
            var deviceViewModel = new DeviceViewModel
            {
                Title = String.Empty,
                DeviceCode = String.Empty,
            };

            // Act
            Func<Task> comparison = async () => { await service.CreateDeviceUsingDapperAsync(deviceViewModel); };

            // Assert
            comparison.Should().NotThrow();
        }

        [Fact]
        public void GetDevices_WithValidParameters_SholdNotThrowAnyExceptions()
        {
            // Arrange

            // Act
            Func<Task> comparison = async () => { await service.GetDevicesUsingDapper(1, 1); };

            // Assert
            comparison.Should().NotThrow();
        }

        [Fact]
        public async void GetDevices_WithValidParameters_ShouldHaveOneElement()
        {
            // Arrange

            // Act
            IList<DeviceViewModel> devices = await service.GetDevicesUsingDapper(1, 1);

            // Assert
            devices.Should().NotBeNull();
            devices.Should().HaveCount(1);
        }
    }
}
