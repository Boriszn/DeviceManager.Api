using DeviceManager.Api.ActionFilters.Settings;
using DeviceManager.Api.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DeviceManager.Api.Configuration.Settings
{
    /// <summary>
    /// Class to read application settings from the appsettings file
    /// </summary>
    public class AppSettings : IValidatable
    {
        /// <summary>
        /// Default thread culture
        /// </summary>
        public string DefaultCulture { get; set; }

        /// <summary>
        /// All supported cultures
        /// </summary>
        public List<string> SupportedCultures { get; set; }
        
        /// <summary>
        /// All supported UI cultures
        /// </summary>
        public List<string> SupportedUiCultures { get; set; }

        /// <inherit/>
        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);

            bool valid = GenericHelper.IsGenericCulture(DefaultCulture);
            SupportedCultures.ForEach(culture => {
                valid &= GenericHelper.IsGenericCulture(culture);
            });
            SupportedUiCultures?.ForEach(culture => {
                valid &= GenericHelper.IsGenericCulture(culture);
            });

            if (!valid)
                throw new ValidationException("Invalid culture code in the config file");
        }
    }
}
