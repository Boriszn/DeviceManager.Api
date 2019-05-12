using DeviceManager.Api.ActionFilters.Settings;
using System.ComponentModel.DataAnnotations;

namespace DeviceManager.Api.Configuration.Settings
{
    /// <summary>
    /// Connection configuration options
    /// </summary>
    public class ConnectionSettings : IValidatable
    {
        /// <summary>
        /// Gets or sets the database type (No sql or MsSql)
        /// </summary>
        public DatabaseType DatabaseType { get; set; }

        /// <summary>
        /// Gets or sets the default connection.
        /// </summary>
        /// <value>
        /// The default connection.
        /// </value>
        [Required]
        public string DefaultConnection { get; set; }

        /// <inherit/>
        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
