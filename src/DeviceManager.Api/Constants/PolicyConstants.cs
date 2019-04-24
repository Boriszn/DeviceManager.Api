using DeviceManager.Api.Configuration;

namespace DeviceManager.Api.Constants
{
    /// <summary>
    /// Define all the policy constants needed for the application. 
    /// These will be used to decorate class/method headers and to define rule in <see cref="AuthenticationConfiguration.Configure"/> method
    /// </summary>
    public static class PolicyConstants
    {
        /// <summary>
        /// Admin policy
        /// </summary>
        public const string Admin = nameof(Admin);

        /// <summary>
        /// Manager policy
        /// </summary>
        public const string Manager = nameof(Manager);
    }
}
