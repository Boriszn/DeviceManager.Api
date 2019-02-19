using System;
using DeviceManager.Api.Model;
using DeviceManager.Api.Resources;
using DeviceManager.Api.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DeviceManager.Api.Services
{
    /// <inheritdoc />
    public class DeviceValidationService : IDeviceValidationService
    {
        private readonly IDeviceViewModelValidationRules deviceViewModelValidationRules;

        private readonly IStringLocalizer<SharedResource> sharedLocalizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceValidationService"/> class.
        /// </summary>
        /// <param name="deviceViewModelValidationRules">The device view model validation rules.</param>
        /// <param name="sharedLocalizer"></param>
        public DeviceValidationService(
            IDeviceViewModelValidationRules deviceViewModelValidationRules, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            this.deviceViewModelValidationRules = deviceViewModelValidationRules;
            this.sharedLocalizer = sharedLocalizer;
        }

        /// <summary>
        /// Validates the specified device view model.
        /// </summary>
        /// <param name="deviceViewModel">The device view model.</param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        public IDeviceValidationService Validate(DeviceViewModel deviceViewModel)
        {
            var validationResult = deviceViewModelValidationRules.Validate(deviceViewModel);

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
        public IDeviceValidationService ValidateDeviceId(Guid deviceId)
        {
            if (deviceId == Guid.Empty)
            {
                throw new ValidationException(string.Format(sharedLocalizer[SharedResource.ShouldNotBeEmpty], "Device"));
            }

            return this;
        }
    }
}
