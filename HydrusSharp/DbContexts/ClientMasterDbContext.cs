using HydrusSharp.Models.ClientMaster;
using System.Data.Entity;

namespace HydrusSharp.DbContexts
{
    public class ClientMasterDbContext : DbContext
    {
        public DbSet<Hash> Hashes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Subtag> Subtags { get; set; }
        public DbSet<Namespace> Namespaces { get; set; }

        public ClientMasterDbContext() : base("ClientMaster")
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasRequired(tag => tag.Subtag).WithMany(subtag => subtag.Tags);
            modelBuilder.Entity<Tag>().HasRequired(tag => tag.Namespace).WithMany(namespaceObject => namespaceObject.Tags);

            base.OnModelCreating(modelBuilder);
        }
    }
}