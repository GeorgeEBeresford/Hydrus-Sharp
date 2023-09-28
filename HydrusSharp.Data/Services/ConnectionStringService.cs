using System.Configuration;
using System.IO;

namespace HydrusSharp.Data.Services
{
    /// <summary>
    /// Provides connection strings that allow paths relative to the hydrus network database directory
    /// </summary>
    public static class ConnectionStringService
    {
        /// <summary>
        /// Gets the value of a configured connection string, replacing the HydrusNetworkDb placeholder with the configured directory name
        /// </summary>
        /// <param name="connectionStringName">
        /// The name of the connection string
        /// </param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionStringName)
        {
            string dbLocation = ConfigurationManager.AppSettings["HydrusNetworkDB"];

            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];


            string fixedConnectionString = connectionStringSettings.ConnectionString.Replace("{HydrusNetworkDB}", dbLocation);
            string fixedDbLocation = fixedConnectionString.Split('=')[1];

            FileInfo fileInfo = new FileInfo(fixedDbLocation);


            return fixedConnectionString;
        }
    }
}
