using System.Threading;
using System.Threading.Tasks;
using DeviceManager.Api.Model;
using FluentValidation;
using FluentValidation.Results;

namespace DeviceManager.Api.Validation
{
    /// <summary>
    /// Interface for ViewModel Validation Rules
    /// </summary>
    public interface IDeviceViewModelValidationRules
    {
        /// <summary>
        /// Validates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        ValidationResult Validate(DeviceViewModel instance);

        /// <summary>
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns></returns>
        Task<ValidationResult> ValidateAsync(DeviceViewModel instance, CancellationToken cancellation);

        /// <summary>
        /// Validates the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        ValidationResult Validate(ValidationContext<DeviceViewModel> context);

        /// <summary>
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns></returns>
        Task<ValidationResult> ValidateAsync(ValidationContext<DeviceViewModel> context, CancellationToken cancellation);
    }
}