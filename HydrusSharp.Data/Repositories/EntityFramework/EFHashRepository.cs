//using HydrusSharp.Data.DbContexts;
//using HydrusSharp.Data.Models.ClientMaster;
//using HydrusSharp.Data.Repositories;
//using System.Linq;

//namespace HydrusSharp.Data.EntityFramework.Repositories
//{
//    public class EFHashRepository : IHashRepository
//    {
//        private ClientMasterDbContext ClientMasterDbContext { get; set; }

//        public EFHashRepository(ClientMasterDbContext dbContext)
//        {
//            ClientMasterDbContext = dbContext;
//        }

//        public Hash GetById(int hashId)
//        {
//            return ClientMasterDbContext.Hashes.First(hash => hash.HashId == hashId);
//        }
//    }
//}