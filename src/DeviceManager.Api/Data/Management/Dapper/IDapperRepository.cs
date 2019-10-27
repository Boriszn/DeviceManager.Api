using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceManager.Api.Data.Management.Dapper
{
    /// <summary>
    /// Generic Repository contract for Dapper
    /// </summary>
    /// <typeparam name="TEntity">Database Entity Type</typeparam>
    public interface IDapperRepository<TEntity>
    {
        /// <summary>
        /// All instances.
        /// </summary>
        /// <param name="page">The identifier.</param>
        /// <param name="pageCount">The identifier.</param>
        /// <returns>Records from the table based on the page number and page count</returns>
        Task<IList<TEntity>> AllAsync(int page, int pageCount);

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Guid id);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task<Guid> AddAsync(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void UpdateAsync(TEntity entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void RemoveAsync(TEntity entity);

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void RemoveAsync(Guid id);
    }
}
