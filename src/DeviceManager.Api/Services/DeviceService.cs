using System;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Data.Model;

namespace DeviceManager.Api.Services
{
    /// <inheritdoc />
    public class DeviceService : IDeviceService
    {
        private readonly IRepository<Device> deviceRepository;

        /// <inheritdoc />
        public DeviceService(IRepository<Device> deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        /// <inheritdoc />
        public Device GetDeviceById(Guid deviceId)
        {
           return  deviceRepository.Get(deviceId);
        }
    }
}
