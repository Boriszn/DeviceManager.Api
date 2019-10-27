using DeviceManager.Api.Attributes.Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeviceManager.Api.Data.Model
{
    /// <summary>
    /// Details of each device. 
    /// Many to One relationship between <see cref="Model.Device"/> and <see cref="DeviceGroup"/>
    /// </summary>
    public class DeviceGroup
    {
        /// <summary>
        /// Primary key
        /// No need to decorate with <see cref="KeyAttribute"/> for the Id property as long as conventions are followed. <see href="https://docs.microsoft.com/en-us/ef/core/modeling/keys#conventions"/>
        /// </summary>
        [Key]
        [DapperInsert]
        public Guid DeviceGroupId { get; set; }

        /// <summary>
        /// Device information
        /// </summary>
        [JsonIgnore]
        public List<Device> Devices { get; set; }

        /// <summary>
        /// Manufactured company
        /// </summary>
        [DapperInsert]
        public string Company { get; set; }

        /// <summary>
        /// Operating system of the device
        /// </summary>
        [DapperInsert]
        public string OperatingSystem { get; set; }
    }
}
