using System.Data;

namespace DeviceManager.Api.Data.Management.Dapper
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        IDbConnection Connection { get; }
    }
}