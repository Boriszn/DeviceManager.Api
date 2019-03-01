using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Services;
using System;
using System.Collections.Generic;
using DeviceManager.Api.Model;
using DeviceManager.Api.UnitTests.Builders;
using DeviceManager.Api.UnitTests.Utils;
using FluentAssertions;
using Xunit;

namespace DeviceManager.Api.UnitTests.Services
{
    public class DeviceServiceTests : IDisposable
    {
        private readonly DeviceService service;

        public DeviceServiceTests()
        {
            // Build Device 
            Device device = new DeviceBuilder()
                .WithId("27be25a2-1b69-4476-a90f-f80498f5e2ec")
                .WithTitle("Raspberry3")
                .Build();

            // Build Device list
            List<Device> devicesList = new List<Device>{ device };

            // Build device service
            service = new DeviceServiceBuilder()
                .WithRepositoryMock(devicesList, device)
                .WithValidationMock()
                .WithUnitOfWorkSetup()
                .Build();
        }

        public void Dispose()
        {
            // TODO: Correct verification 
            // this.mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateDevice_WithValidParameters_SholdNotTrowAnyExceptions()
        {
            // Arrange
            var deviceViewModel = new DeviceViewModel
            {
                Title = String.Empty,
                DeviceCode = String.Empty,
            };

            // Act
            Action comparison = () => { service.CreateDevice(deviceViewModel); };

            // Assert
            comparison.Should().NotThrow();
        }

        [Fact]
        public void GetDevices_WithValidParameters_SholdNotTrowAnyExceptions()
        {
            // Arrange

            // Act
            Action comparison = () => { service.GetDevices(1,1); };

            // Assert
            comparison.Should().NotThrow();
        }

        [Fact]
        public void GetDevices_WithValidParameters_ShouldHaveOneElement()
        {
            // Arrange

            // Act
            List<DeviceViewModel> devices = service.GetDevices(1,1);

            // Assert
            devices.Should().NotBeNull();
            devices.Should().HaveCount(1);
        }

        [Fact]
        public void GetDeviceById_WithValidParameters_SholdNotBeNull()
        {
            // Arrange
            Device deviceArrange = new DeviceBuilder()
                .WithId("27be25a2-1b69-4476-a90f-f80498f5e2ec")
                .WithTitle("Raspberry3")
                .Build();

            // Act
            DeviceViewModel device = service.GetDeviceById(deviceArrange.DeviceId);

            // Assert
            device.Should().NotBeNull();
            device.Id.Should().Be(deviceArrange.DeviceId);
            device.Title.Should().Be(deviceArrange.DeviceTitle);
        }

        [Fact]
        public void UpdateDevice_WithValidParameters_SholdNotThrowAnyExceptions()
        {
            // Arrange
            Device deviceArrange = new DeviceBuilder()
                .WithId("27be25a2-1b69-4476-a90f-f80498f5e2ec")
                .WithTitle("Raspberry3")
                .Build();

            DeviceViewModel deviceViewModel = new DeviceViewModelBuilder()
                .WithCode("12314")
                .WithTitle("Raspberry3")
                .Build();

            // Act
            Action action = () => { service.UpdateDevice(deviceArrange.DeviceId, deviceViewModel); };

            // Assert
            action.Should().NotThrow();
        }
    }
}
