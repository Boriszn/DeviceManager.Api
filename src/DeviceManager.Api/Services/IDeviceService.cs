using System;
using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Model;

namespace DeviceManager.Api.Services
{
    /// <summary>
    /// Device service interface
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// Gets the device by identifier.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        Device GetDeviceById(Guid deviceId);

        /// <summary>
        /// Gets the device by title.
        /// </summary>
        /// <param name="deviceTitle">The device title.</param>
        /// <returns></returns>
        Device GetDeviceByTitle(string deviceTitle);

        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="deviceViewModel">The device view model.</param>
        void CreateDevice(DeviceViewModel deviceViewModel);

        /// <summary>
        /// Updates the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceViewModel">The device view model.</param>
        void UpdateDevice(Guid deviceId, DeviceViewModel deviceViewModel);
    }
}