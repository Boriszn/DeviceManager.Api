using System;
using DeviceManager.Api.Data.Model;

namespace DeviceManager.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// Gets the device by identifier.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        Device GetDeviceById(Guid deviceId);
    }
}