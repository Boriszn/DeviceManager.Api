using DeviceManager.Api.Attributes.Dapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeviceManager.Api.Data.Model
{
    /// <summary>
    /// The Device data model
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        [Key()]
        [DapperInsert]
        public Guid DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the device title.
        /// </summary>
        /// <value>
        /// The device title.
        /// </value>
        [DapperInsert]
        public string DeviceTitle { get; set; }

        /// <summary>
        /// Device code
        /// </summary>
        [DapperInsert]
        public string DeviceCode { get; set; }

        /// <summary>
        /// Device group id
        /// </summary>
        [DapperInsert]
        public Guid DeviceGroupId { get; set; }

        /// <summary>
        /// Device group details
        /// </summary>
        public DeviceGroup DeviceGroup { get; set; }
    }
}