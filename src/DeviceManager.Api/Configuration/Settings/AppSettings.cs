using System.Collections.Generic;
using System.Globalization;

namespace DeviceManager.Api.Configuration.Settings
{
    /// <summary>
    /// Class to read application settings from the appsettings file
    /// </summary>
    public class AppSettings
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
    }
}
