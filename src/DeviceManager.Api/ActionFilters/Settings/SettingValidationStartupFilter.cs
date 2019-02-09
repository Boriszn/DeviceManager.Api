using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;

namespace DeviceManager.Api.ActionFilters.Settings
{
    /// <summary>
    /// Validates all values in the app settings files. Implemented based on <see href="https://andrewlock.net/adding-validation-to-strongly-typed-configuration-objects-in-asp-net-core/"/>
    /// </summary>
    public class SettingValidationStartupFilter : IStartupFilter
    {
        readonly IEnumerable<IValidatable> _validatableObjects;

        /// <summary>
        /// Gets all validatable objects. ALl configuration classes implementing IValidatable
        /// </summary>
        /// <param name="validatableObjects"></param>
        public SettingValidationStartupFilter(IEnumerable<IValidatable> validatableObjects)
        {
            _validatableObjects = validatableObjects;
        }

        ///<inherit/>
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            foreach (var validatableObject in _validatableObjects)
            {
                validatableObject.Validate();
            }

            //don't alter the configuration
            return next;
        }
    }
}