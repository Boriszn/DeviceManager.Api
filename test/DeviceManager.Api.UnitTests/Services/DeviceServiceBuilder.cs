using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using DeviceManager.Api.Services;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Mappings;
using DeviceManager.Api.Model;
using DeviceManager.Api.Validation;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DeviceManager.Api.UnitTests.Services
{
    public class DeviceServiceBuilder
    {
        private readonly Mock<IRepository<Device>> mockRepository;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly MapperConfiguration mapperConfiguration;
        private readonly Mapper mapper;
        private Mock<IDeviceValidationService> mockDeviceValidationService;

        public DeviceServiceBuilder()
        {
            var mockRepositoryObjet = new MockRepository(MockBehavior.Strict);

            this.mockDeviceValidationService = mockRepositoryObjet.Create<IDeviceValidationService>();

            mockUnitOfWork = mockRepositoryObjet.Create<IUnitOfWork>();
            mockRepository = mockRepositoryObjet.Create<IRepository<Device>>();

            // Default automapper configuration
            mapperConfiguration = new MapperConfiguration(new MapsProfile());
            mapper = new Mapper(mapperConfiguration);
        }

        /// <summary>
        /// With the repository methods mock.
        /// </summary>
        /// <param name="devicesList">The devices list.</param>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        public DeviceServiceBuilder WithRepositoryMock(List<Device> devicesList, Device device)
        {
            // 'GetAll' repository mock
            this.mockRepository.Setup(x => x.GetAll(1, 1)).Returns(devicesList.AsQueryable);

            // 'Get' repository mock
            this.mockRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(device);

            // 'Update' repository mock
            this.mockRepository.Setup(x => x.Update(It.IsAny<Device>())).Returns(It.IsAny<EntityState>());

            // 'Add' repository mock
            this.mockRepository.Setup(x => x.Add(It.IsAny<Device>())).Returns(EntityState.Added);

            // 'FindBy' repository mock
            this.mockRepository.Setup(x => x.FindBy(It.IsAny<Expression<Func<Device, bool>>>()))
                .Returns(() => 
                    devicesList.AsQueryable());

            return this;
        }

        /// <summary>
        /// With the repository GetAll mock.
        /// </summary>
        /// <param name="devicesList">The devices list.</param>
        /// <returns></returns>
        public DeviceServiceBuilder WithRepositoryGetAllMock(List<Device> devicesList)
        {
            // 'GetAll' repository mock
            this.mockRepository.Setup(x => x.GetAll()).Returns(devicesList.AsQueryable);

            return this;
        }

        /// <summary>
        /// With the repository get all mock.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="devicesList">The devices list.</param>
        /// <returns></returns>
        public DeviceServiceBuilder WithRepositoryGetAllMock(int page, int pageSize, List<Device> devicesList)
        {
            // 'GetAll' repository mock
            this.mockRepository.Setup(x => x.GetAll(page, pageSize)).Returns(devicesList.AsQueryable);

            return this;
        }

        /// <summary>
        /// With the repository Get mock.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        public DeviceServiceBuilder WithRepositoryGetMock(Device device)
        {
            // 'Get' repository mock
            this.mockRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(device);

            return this;
        }

        /// <summary>
        /// With the unit of work setup.
        /// </summary>
        /// <returns></returns>
        public DeviceServiceBuilder WithUnitOfWorkSetup()
        {
            this.mockUnitOfWork.Setup(x => x.Commit()).Returns(1);
            this.mockUnitOfWork.Setup(u => u.GetRepository<Device>()).Returns(this.mockRepository.Object);
            return this;
        }

        public DeviceServiceBuilder WithValidationMock()
        {
            this.mockDeviceValidationService
                .Setup(x => x.Validate(It.IsAny<DeviceViewModel>()))
                .Returns(new DeviceValidationService(new DeviceViewModelValidationRules()));

            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public DeviceService Build()
        {
            return new DeviceService(
                this.mockUnitOfWork.Object,
                this.mockDeviceValidationService.Object,
                mapper);
        }
    }
}