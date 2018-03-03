using DeviceManager.Api.Model;
using FluentValidation;

namespace DeviceManager.Api.Validation
{
    public class DeviceViewModelValidationRules : AbstractValidator<DeviceViewModel>, IDeviceViewModelValidationRules
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceViewModelValidationRules"/> class.
        /// <example>
        /// All validation rules can be found here: https://github.com/JeremySkinner/FluentValidation/wiki/a.-Index
        /// </example>
        /// </summary>
        public DeviceViewModelValidationRules()
        {
            RuleFor(device => device.DeviceCode)
                .NotEmpty()
                .Length(5, 10);

            RuleFor(device => device.DeviceCode)
                .NotEmpty();

            RuleFor(device => device.Title)
                .NotEmpty();
        }
    }
}
