using System;
using System.Collections.Generic;
using System.Linq;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Model;

namespace DeviceManager.Api.Services
{
    /// <inheritdoc />
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork unitOfWork;

        /// <inheritdoc />
        public DeviceService(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public List<Device> GetDevices()
        {
            var deviceRepository = unitOfWork.GetRepository<Device>();

            return deviceRepository.GetAll().ToList();
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

            // Add new device
            deviceRepository.Add(
                new Device
                {
                    DeviceId = Guid.NewGuid(),
                    DeviceTitle = deviceViewModel.Title
                });

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

            // Update device properties
            device.DeviceTitle = deviceViewModel.Title;

            deviceRepository.Update(device);

            // Commit changes
            unitOfWork.Commit();
        }
    }
}
