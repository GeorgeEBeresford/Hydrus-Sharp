using HydrusSharp.DbContexts;
using HydrusSharp.Models.ClientMappings;
using System.Collections.Generic;
using System.Linq;

namespace HydrusSharp.Providers
{
    public class MappingRepository
    {
        private ClientMappingsDbContext DbContext { get; set; }

        public MappingRepository(ClientMappingsDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IEnumerable<ICurrentMapping> GetAll()
        {
            IEnumerable<ICurrentMapping> currentMappings = DbContext.CurrentMappings10;
            currentMappings = currentMappings.Union(DbContext.CurrentMappings9);

            return currentMappings;
        }

        public IEnumerable<ICurrentMapping> GetByTagId(int tagId)
        {
            return GetAll().Where(mapping => mapping.TagId == tagId);
        }

        public IEnumerable<ICurrentMapping> GetByTagIds(IEnumerable<int> tagIds)
        {
            return GetAll().Where(mapping => tagIds.Any(tagId => tagId == mapping.TagId));
        }

        public IEnumerable<ICurrentMapping> GetByMissingTagIds(IEnumerable<int> missingTagIds)
        {
            return GetAll().Where(mapping => missingTagIds.All(tagId => tagId != mapping.TagId)); ;
        }
    }
}