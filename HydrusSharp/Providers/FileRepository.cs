using HydrusSharp.DbContexts;
using HydrusSharp.Enums;
using HydrusSharp.Models.Client;
using HydrusSharp.Models.ClientMappings;
using HydrusSharp.Models.ClientMaster;
using HydrusSharp.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace HydrusSharp.Providers
{
    public class FileRepository
    {
        private ClientDbContext ClientDbContext { get; set; }
        private ClientMasterDbContext ClientMasterDbContext { get; set; }
        private ClientMappingsDbContext ClientMappingsDbContext { get; set; }

        public FileRepository(ClientDbContext clientDbContext, ClientMasterDbContext clientMasterDbContext, ClientMappingsDbContext clientMappingsDbContext)
        {
            ClientDbContext = clientDbContext;
            ClientMasterDbContext = clientMasterDbContext;
            ClientMappingsDbContext = clientMappingsDbContext;
        }

        public IEnumerable<FileInfo> GetFileInfos(MediaCollectViewModel collect, MediaSortViewModel sort, SearchPredicateViewModel[] filters)
        {
            IEnumerable<FileInfo> matchingFiles = ClientDbContext.FileInfos;

            // Filter the file by any specific filters before ordering by non-specific filters. Reduce the number of mappings needed
            IEnumerable<SearchPredicateViewModel> orderedFilters = filters.OrderBy(filter => filter.MustBeTrue);


            foreach (SearchPredicateViewModel filter in orderedFilters)
            {
                if (filter.SearchType == PredicateType.PREDICATE_TYPE_TAG)
                {
                    matchingFiles = FilterByTag(matchingFiles, (string)filter.SearchData[0], filter.MustBeTrue);
                }
            }

            return matchingFiles.ToList();
        }

        private IEnumerable<FileInfo> FilterByTag(IEnumerable<FileInfo> files, string requiredTag, bool mustBeTrue)
        {
            MappingRepository mappingRepository = new MappingRepository(ClientMappingsDbContext);
            IEnumerable<int> matchingTagIds = GetMatchingTagIds(requiredTag);

            if (mustBeTrue)
            {
                List<ICurrentMapping> mappings = mappingRepository.GetByTagIds(matchingTagIds).ToList();
                return files.Where(file => mappings.Any(mapping => mapping.HashId == file.HashId));
            }

            List<ICurrentMapping> nonMappings = mappingRepository.GetByMissingTagIds(matchingTagIds).ToList();
            return files.Where(file => nonMappings.Any(mapping => mapping.HashId == file.HashId));
        }

        /// <summary>
        /// Retrieves any parents and siblings of the tag (along with the original tag)
        /// </summary>
        /// <param name="searchedTags"></param>
        /// <returns></returns>
        private IEnumerable<int> GetMatchingTagIds(string tagName)
        {
            TagRepository tagRepository = new TagRepository(ClientMasterDbContext, ClientDbContext);

            Tag matchingTag = tagRepository.GetTag(tagName);
            IEnumerable<Tag> matchingChildren = tagRepository.GetTagChildren(matchingTag.TagId).ToList();

            // Search for any files which match the tag as a direct mapping, parent mapping or sibling mapping
            List<int> searchedTagIds = matchingChildren.Select(tag => tag.TagId).ToList();
            searchedTagIds.Add(matchingTag.TagId);

            return searchedTagIds;
        }
    }
}