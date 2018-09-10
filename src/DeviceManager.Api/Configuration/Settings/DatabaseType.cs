using System.ComponentModel;

namespace DeviceManager.Api.Configuration.Settings
{
    /// <summary>
        /// Gets or sets the database type (No sql or Sql express)
        /// </summary>
    public enum DatabaseType 
    {
        /// <summary>
        /// Sql express Database type
        /// </summary>
        SqlExpress = 0,

        /// <summary>
        /// Postgres Database type
        /// </summary>
        Postgres = 1,

        /// <summary>
        /// Mongodb database type
        /// </summary>
        // MongoDb = 2
    }
}
