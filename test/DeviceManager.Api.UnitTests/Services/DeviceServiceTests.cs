using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Services;
using System.Linq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DeviceManager.Api.Model;
using DeviceManager.Api.UnitTests.Utils;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DeviceManager.Api.UnitTests.Services
{
    public class DeviceServiceTests : IDisposable
    {
        private readonly Mock<IRepository<Device>>  mockRepository;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;

        private readonly DeviceService service;

        public DeviceServiceTests()
        {
            var mockRepositoryObjet = new MockRepository(MockBehavior.Strict);

            mockUnitOfWork = mockRepositoryObjet.Create<IUnitOfWork>();
            mockRepository = mockRepositoryObjet.Create<IRepository<Device>>();

            service = this.CreateService();
        }

        public void Dispose()
        {
            // TODO: Correct verification 
            // this.mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateDevie_WithValidParameters_SholdNotTrowAnyExceptions()
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
            Action comparison = () => { service.GetDevices(); };

            // Assert
            comparison.Should().NotThrow();
        }

        [Fact]
        public void GetDevices_WithValidParameters_ShouldHaveOneElement()
        {
            // Arrange

            // Act
            List<Device> devices = service.GetDevices();

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
            Device device = service.GetDeviceById(deviceArrange.DeviceId);

            // Assert
            device.Should().NotBeNull();
            device.DeviceId.Should().Be(deviceArrange.DeviceId);
            device.DeviceTitle.Should().Be(deviceArrange.DeviceTitle);
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

        private DeviceService CreateService()
        {
            var device = new DeviceBuilder()
                .WithId("27be25a2-1b69-4476-a90f-f80498f5e2ec")
                .WithTitle("Raspberry3")
                .Build();

            var devicesList = new List<Device>{ device };

            this.mockRepository.Setup(x => x.GetAll()).Returns(devicesList.AsQueryable);
            this.mockRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(device);
            this.mockRepository.Setup(x => x.Update(It.IsAny<Device>())).Returns(It.IsAny<EntityState>());
            this.mockRepository.Setup(x => x.Add(It.IsAny<Device>())).Returns(EntityState.Added);

            this.mockRepository.Setup(x => x.FindBy(It.IsAny<Expression<Func<Device, bool>>>()))
                .Returns(() => devicesList.AsQueryable());

            this.mockUnitOfWork.Setup(x => x.Commit()).Returns(1);
            this.mockUnitOfWork.Setup(u => u.GetRepository<Device>()).Returns(this.mockRepository.Object);

            return new DeviceService(
                this.mockUnitOfWork.Object);
        }
    }
}
