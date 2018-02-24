using System;
using DeviceManager.Api.Model;
using DeviceManager.Api.Validation;
using FluentValidation;

namespace DeviceManager.Api.Services
{
    /// <inheritdoc />
    public class DeviceValidationService : IDeviceValidationService
    {
        /// <summary>
        /// Validates the specified device view model.
        /// </summary>
        /// <param name="deviceViewModel">The device view model.</param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        public DeviceValidationService Validate(DeviceViewModel deviceViewModel)
        {
            var validationResult = new DeviceViewModelValidationRules().Validate(deviceViewModel);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return this;
        }

        /// <summary>
        /// Validates the device identifier.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        /// <exception cref="ValidationException">Shuld not be empty</exception>
        public DeviceValidationService ValidateDeviceId(Guid deviceId)
        {
            if (deviceId == Guid.Empty)
            {
                throw new ValidationException("Shuld not be empty");
            }

            return this;
        }
    }
}
