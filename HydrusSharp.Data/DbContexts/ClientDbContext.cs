//using HydrusSharp.Data.Models.Client;
//using HydrusSharp.Data.Services;
//using System.Data.Entity;

//namespace HydrusSharp.Data.DbContexts
//{
//	public class ClientDbContext : DbContext
//    {
//        public DbSet<OptionCollection> Options { get; set; }
//        public DbSet<JsonDump> JsonDumps { get; set; }
//		public DbSet<NamedJsonDump> NamedJsonDumps { get; set; }
//		public DbSet<HashedJsonDump> HashedJsonDumps { get; set; }
//		public DbSet<FileInfo> FileInfos { get; set; }
//		public DbSet<TagParent> TagParents { get; set; }

//        public ClientDbContext(): base(ConnectionStringService.GetConnectionString("Client")) { 

//		}

//		// https://stackoverflow.com/a/19130718
//		public void FixEfProviderServicesProblem()
//		{
//			//The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
//			//for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
//			//Make sure the provider assembly is available to the running application. 
//			//See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

//			var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
//		}
//	}
//}