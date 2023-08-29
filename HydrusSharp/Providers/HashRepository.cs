using HydrusSharp.DbContexts;
using HydrusSharp.Models.ClientMaster;
using System.Linq;

namespace HydrusSharp.Providers
{
    public class HashRepository
    {
        private ClientMasterDbContext ClientMasterDbContext { get; set; }

        public HashRepository(ClientMasterDbContext dbContext)
        {
            ClientMasterDbContext = dbContext;
        }

        public Hash GetById(int hashId)
        {
            return ClientMasterDbContext.Hashes.First(hash => hash.HashId == hashId);
        }
    }
}