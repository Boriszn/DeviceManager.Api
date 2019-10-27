using System.Data;

namespace DeviceManager.Api.Data.Management.Dapper
{
    /// <summary>
    /// Base dapper repository
    /// </summary>
    public abstract class DapperRepositoryBase
    {
        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        protected IDbTransaction Transaction { get; }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        protected IDbConnection Connection => Transaction.Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DapperRepositoryBase"/> class.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        protected DapperRepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
