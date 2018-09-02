namespace DeviceManager.Api.Configuration.Settings
{
    /// <summary>
    /// Connection configuration options
    /// </summary>
    public class ConnectionSettings
    {
        /// <summary>
        /// Gets or sets the database type (No sql or Sql express)
        /// </summary>
        public bool UseNoSql { get; set; }

        /// <summary>
        /// Gets or sets the default connection.
        /// </summary>
        /// <value>
        /// The default connection.
        /// </value>
        public string DefaultConnection { get; set; }
    }
}
