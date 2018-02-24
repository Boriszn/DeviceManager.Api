using System;
using DeviceManager.Api.Model;
using FluentValidation;

namespace DeviceManager.Api.Services
{
    /// <summary>
    /// Validation service for Device model
    /// </summary>
    public interface IDeviceValidationService
    {
        /// <summary>
        /// Validates the specified device view model.
        /// </summary>
        /// <param name="deviceViewModel">The device view model.</param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        DeviceValidationService Validate(DeviceViewModel deviceViewModel);

        /// <summary>
        /// Validates the device identifier.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        /// <exception cref="ValidationException">Shuld not be empty</exception>
        DeviceValidationService ValidateDeviceId(Guid deviceId);
    }
}