using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Model;

namespace DeviceManager.Api.Services
{
    /// <inheritdoc />
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <inheritdoc />
        public DeviceService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public List<Device> GetDevices(int page, int pageSize)
        {
            var deviceRepository = unitOfWork.GetRepository<Device>();

            return deviceRepository.GetAll(page, pageSize).ToList();
        }

        /// <inheritdoc />
        public Device GetDeviceById(Guid deviceId)
        {
            var deviceRepository = unitOfWork.GetRepository<Device>();

            return deviceRepository.Get(deviceId);
        }

        /// <inheritdoc />
        public Device GetDeviceByTitle(string deviceTitle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void CreateDevice(DeviceViewModel deviceViewModel)
        {
            var deviceRepository = unitOfWork.GetRepository<Device>();

            deviceRepository.Add(mapper.Map<DeviceViewModel, Device>(deviceViewModel));

            // Commit changes
            unitOfWork.Commit();
        }

        /// <inheritdoc />
        public void UpdateDevice(Guid deviceId, DeviceViewModel deviceViewModel)
        {
            var deviceRepository = unitOfWork.GetRepository<Device>();

            // Get device
            Device device = deviceRepository.Get(deviceId);

            if (device == null)
            {
                throw new NullReferenceException();
            }

            device.DeviceTitle = deviceViewModel.Title;

            deviceRepository.Update(device);

            // Commit changes
            unitOfWork.Commit();
        }
    }
}
