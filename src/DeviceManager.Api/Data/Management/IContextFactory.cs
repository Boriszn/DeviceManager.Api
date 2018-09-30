namespace DeviceManager.Api.Data.Management
{
    /// <summary>
    /// Context factory interface
    /// </summary>
    public interface IContextFactory
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        IDbContext DbContext { get; }
    }
}