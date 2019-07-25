namespace DeviceManager.Api.Data.Management.Dapper
{
    /// <summary>
    /// Represents repository/object creation and transaction management
    /// </summary>
    public interface IDapperUnitOfWork
    {
        /// <summary>
        /// Gets the device repository.
        /// </summary>
        /// <value>
        /// The device repository.
        /// </value>
        IDapperRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        void Dispose();
    }
}
