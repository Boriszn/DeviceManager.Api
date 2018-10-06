using System.ComponentModel.DataAnnotations;

namespace DeviceManager.Api.Model
{
    /// <summary>
    /// View model for Device
    /// </summary>
    public class DeviceViewModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the device code.
        /// </summary>
        /// <value>
        /// The device code.
        /// </value>
        public string DeviceCode { get; set; }

        /// <summary>
        /// Optional details
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Operating system of the device
        /// </summary>
        public string OperatingSystem { get; set; }
    }
}