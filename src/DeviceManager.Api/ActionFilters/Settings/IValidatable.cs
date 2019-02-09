namespace DeviceManager.Api.ActionFilters.Settings
{
    /// <summary>
    /// Configuration classes which want to validate configuration data at compile time should implement this interface
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// The classes should provide validation logic
        /// </summary>
        void Validate();
    }
}