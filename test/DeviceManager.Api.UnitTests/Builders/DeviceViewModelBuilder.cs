using DeviceManager.Api.Model;

namespace DeviceManager.Api.UnitTests.Utils
{
    public class DeviceViewModelBuilder
    {
        private string code;
        private string title;

        public DeviceViewModelBuilder()
        {
        }

        /// <summary>
        /// With the code.
        /// </summary>
        /// <param name="newCode">The new code.</param>
        /// <returns></returns>
        public DeviceViewModelBuilder WithCode(string newCode)
        {
            this.code = newCode;
            return this;
        }

        /// <summary>
        /// With the title.
        /// </summary>
        /// <param name="newTitle">The new title.</param>
        /// <returns></returns>
        public DeviceViewModelBuilder WithTitle(string newTitle)
        {
            this.title = newTitle;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public DeviceViewModel Build()
        {
            return new DeviceViewModel
            {
                Title = this.title,
                DeviceCode = this.code
            };
        }
    }
}