using HydrusSharp.Models.ClientMappings;
using System.Data.Entity;

namespace HydrusSharp.DbContexts
{
    public class ClientMappingsDbContext : DbContext
    {
        public DbSet<CurrentMappings9> CurrentMappings9 { get; set; }
        public DbSet<CurrentMappings10> CurrentMappings10 { get; set; }

        public ClientMappingsDbContext() : base("ClientMappings")
        {

        }

        // https://stackoverflow.com/a/19130718
        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}