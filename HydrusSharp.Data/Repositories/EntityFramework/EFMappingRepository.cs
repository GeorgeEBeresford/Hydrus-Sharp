//using HydrusSharp.Data.DbContexts;
//using HydrusSharp.Data.Models.ClientMappings;
//using HydrusSharp.Data.Repositories;
//using System.Collections.Generic;
//using System.Linq;

//namespace HydrusSharp.Data.EntityFramework.Repositories
//{
//    public class EFMappingRepository : IMappingRepository
//    {
//        private ClientMappingsDbContext DbContext { get; set; }

//        public EFMappingRepository(ClientMappingsDbContext dbContext)
//        {
//            DbContext = dbContext;
//        }

//        public IEnumerable<CurrentMapping> GetCurrentMappings(int serviceId)
//        {
//            // SQL columns must match the class property names, not the column names
//            IEnumerable<CurrentMapping> queryableMappings = DbContext.CurrentMappings.SqlQuery($"SELECT tag_id AS TagId, hash_id AS HashId FROM current_mappings_{serviceId}").AsQueryable();

//            return queryableMappings;
//        }
//    }
//}