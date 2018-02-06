using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Services;
using System.Linq;
using Moq;
using System;
using System.Collections.Generic;
using DeviceManager.Api.Model;
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
           // this.mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateDevie_WithValidParameters_SholdNotTrowAnyExceptions()
        {
            // Arrange
            var deviceViewModel = new DeviceViewModel
            {
                Title = " ",
                DeviceCode = " ",
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
            
            // int page = 1
            // int pageCount = 5;

            // Act
            Action comparison = () => { service.GetDevices(); };

            // Assert
            comparison.Should().NotThrow();
        }

        private DeviceService CreateService()
        {
            var devicesList = new List<Device>
            {
                new Device { DeviceId = Guid.NewGuid(), DeviceTitle = "Raspberry-3"}
            };

            this.mockRepository.Setup(x => x.GetAll()).Returns(devicesList.AsQueryable);
            this.mockRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(new Device());
            this.mockRepository.Setup(x => x.Add(It.IsAny<Device>())).Returns(EntityState.Added);
            this.mockUnitOfWork.Setup(x => x.Commit()).Returns(1);

            return new DeviceService(
                this.mockRepository.Object,
                this.mockUnitOfWork.Object);
        }
    }
}
