//using HydrusSharp.Core.Enums;
//using HydrusSharp.Data.DbContexts;
//using HydrusSharp.Data.Models.Client;
//using HydrusSharp.Data.Models.ClientMappings;
//using HydrusSharp.Data.Models.ClientMaster;
//using HydrusSharp.Data.Models.ViewModels;
//using HydrusSharp.Data.Repositories;
//using System.Collections.Generic;
//using System.Linq;

//namespace HydrusSharp.Data.EntityFramework.Repositories
//{
//    public class EFFileRepository : IFileRepository
//    {
//        private ClientDbContext ClientDbContext { get; set; }
//        private ClientMasterDbContext ClientMasterDbContext { get; set; }
//        private ClientMappingsDbContext ClientMappingsDbContext { get; set; }

//        public EFFileRepository(ClientDbContext clientDbContext, ClientMasterDbContext clientMasterDbContext, ClientMappingsDbContext clientMappingsDbContext)
//        {
//            ClientDbContext = clientDbContext;
//            ClientMasterDbContext = clientMasterDbContext;
//            ClientMappingsDbContext = clientMappingsDbContext;
//        }

//        public IEnumerable<FileInfo> GetFileInfos(MediaCollectViewModel collect, MediaSortViewModel sort, SearchPredicateViewModel[] filters)
//        {
//            IEnumerable<FileInfo> matchingFiles = ClientDbContext.FileInfos;

//            // Filter the file by any specific filters before ordering by non-specific filters. Reduce the number of mappings needed
//            IEnumerable<SearchPredicateViewModel> orderedFilters = filters.OrderBy(filter => filter.MustBeTrue);


//            foreach (SearchPredicateViewModel filter in orderedFilters)
//            {
//                if (filter.SearchType == PredicateType.PREDICATE_TYPE_TAG)
//                {
//                    matchingFiles = FilterByTag(matchingFiles, (string)filter.SearchData[0], filter.MustBeTrue);
//                }
//            }

//            return matchingFiles;
//        }

//        private IEnumerable<FileInfo> FilterByTag(IEnumerable<FileInfo> files, string requiredTag, bool mustBeTrue)
//        {
//            IMappingRepository mappingRepository = new EFMappingRepository(ClientMappingsDbContext);
//            IEnumerable<int> matchingTagIds = GetMatchingTagIds(requiredTag);
//            IEnumerable<CurrentMapping> currentMappings = mappingRepository.GetCurrentMappings(9)
//                .Union(mappingRepository.GetCurrentMappings(10));

//            if (mustBeTrue)
//            {
//                IEnumerable<CurrentMapping> matchingMappings = currentMappings
//                    .Where(mapping => matchingTagIds.Any(tagId => tagId == mapping.TagId))
//                    .ToList();

//                return files.Where(file => matchingMappings.Any(mapping => mapping.HashId == file.HashId));
//            }

//            IEnumerable<CurrentMapping> nonMappingIds = currentMappings
//                    .Where(mapping => matchingTagIds.All(tagId => tagId == mapping.TagId));

//            return files.Where(file => nonMappingIds.Any(nonMapping => nonMapping.HashId == file.HashId));
//        }

//        /// <summary>
//        /// Retrieves any parents and siblings of the tag (along with the original tag)
//        /// </summary>
//        /// <param name="searchedTags"></param>
//        /// <returns></returns>
//        private IEnumerable<int> GetMatchingTagIds(string tagName)
//        {
//            ITagRepository tagRepository = new EFTagRepository(ClientMasterDbContext, ClientDbContext);

//            Tag matchingTag = tagRepository.GetTag(tagName);
//            IEnumerable<Tag> matchingChildren = tagRepository.GetTagChildren(matchingTag.TagId).ToList();

//            // Search for any files which match the tag as a direct mapping, parent mapping or sibling mapping
//            List<int> searchedTagIds = matchingChildren.Select(tag => tag.TagId).ToList();
//            searchedTagIds.Add(matchingTag.TagId);

//            return searchedTagIds;
//        }
//    }
//}