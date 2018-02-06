using System;
using DeviceManager.Api.Data.Model;

namespace DeviceManager.Api.UnitTests.Utils
{
    public class DeviceBuilder
    {
        private Guid deviceId;
        private string title;

        public DeviceBuilder()
        {
            deviceId = Guid.Empty;
        }

        /// <summary>
        /// With the identifier.
        /// </summary>
        /// <param name="newId">The new identifier.</param>
        /// <returns></returns>
        public DeviceBuilder WithId(string newId)
        {
            this.deviceId = Guid.Parse(newId);
            return this;
        }

        /// <summary>
        /// With the title.
        /// </summary>
        /// <param name="newTitle">The new title.</param>
        /// <returns></returns>
        public DeviceBuilder WithTitle(string newTitle)
        {
            this.title = newTitle;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public Device Build()
        {
            return new Device
            {
                DeviceId = this.deviceId,
                DeviceTitle = this.title,
            };
        }
    }
}