using DeviceManager.Api.Model;
using FluentValidation;

namespace DeviceManager.Api.Validation
{
    public class DeviceViewModelValidationRules : AbstractValidator<DeviceViewModel>
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
                .Length(5, 10)
                .WithMessage("DeviceCode should be min 5 and max 10 character");

            RuleFor(device => device.DeviceCode)
                .NotEmpty()
                .WithMessage("DeviceCode should not be null");

            RuleFor(device => device.Title)
                .NotEmpty()
                .WithMessage("Device title should not be null");
        }
    }
}
