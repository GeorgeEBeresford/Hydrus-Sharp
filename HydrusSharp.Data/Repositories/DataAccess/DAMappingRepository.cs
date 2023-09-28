using DataAccessLayer;
using HydrusSharp.Core.Enums;
using HydrusSharp.Data.Models.ClientMappings;
using HydrusSharp.Data.Models.ClientMaster;
using HydrusSharp.Data.Models.ViewModels;
using HydrusSharp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HydrusSharp.Data.Repositories.DataAccess
{
    public class DAMappingRepository : IMappingRepository
    {
        public IEnumerable<CurrentMapping> GetCurrentMappings(int serviceId)
        {
            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMappings")))
            {
                IEnumerable<DbRow> rows = dbContext.GetDataRows($"SELECT * FROM current_mappings_{serviceId}");

                return rows.Select(row => new CurrentMapping
                {
                    HashId = Convert.ToInt32(row.GetColumnValue("hash_id")),
                    TagId = Convert.ToInt32(row.GetColumnValue("tag_id"))
                });
            }
        }

        public int GetMappingsCount(SearchPredicateViewModel[] filters)
        {
            string mappingsSql = $"WITH FilteredMappings AS ({GenerateFilteringSql(filters)}) SELECT COUNT(*) AS TotalItems FROM FilteredMappings";

            DbRow mappingRow;
            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMappings")))
            {
                mappingRow = dbContext.GetDataRow(mappingsSql);
            }

            return Convert.ToInt32(mappingRow.GetColumnValue("TotalItems"));
        }

        public IEnumerable<CurrentMapping> GetMatchingMappings(MediaCollectViewModel collect, MediaSortViewModel sort, SearchPredicateViewModel[] filters, int skip, int take)
        {
            string mappingsSql = GenerateFilteringSql(filters) + $" LIMIT {take} OFFSET {skip}";

            IEnumerable<DbRow> mappingRows;
            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMappings")))
            {
                mappingRows = dbContext.GetDataRows(mappingsSql);
            }

            return mappingRows
                .Select(row => new CurrentMapping
                {
                    HashId = Convert.ToInt32(row.GetColumnValue("hash_id")),
                    TagId = Convert.ToInt32(row.GetColumnValue("tag_id"))
                });
        }

        private string GenerateFilteringSql(SearchPredicateViewModel[] filters)
        {
            // Todo - Filters should eventually specify which services to retrieve from
            int[] searchedServices = new[] { 9, 10 };

            // We want to return mappings from all of the requested services
            string[] searchedServiceSqls = searchedServices.Select(serviceId => $"SELECT * FROM current_mappings_{serviceId} WHERE 1=1").ToArray();

            foreach (SearchPredicateViewModel filter in filters)
            {
                if (filter.SearchType == PredicateType.PREDICATE_TYPE_TAG)
                {
                    IEnumerable<int> matchingTags = GetMatchingTagIds((string)filter.SearchData[0]);
                    
                    for (int sqlIndex = 0; sqlIndex < searchedServiceSqls.Length; sqlIndex++)
                    {
                        if (!matchingTags.Any())
                        {
                            // We couldn't actually find the tag we're looking for. Logically, there can be no mappings that match a non-existent tag
                            searchedServiceSqls[sqlIndex] += " AND 1=2";
                        }
                        else if (filter.MustBeTrue)
                        {
                            searchedServiceSqls[sqlIndex] += $" AND (tag_id IN ({string.Join(",", matchingTags)}) OR tag_id IN ({string.Join(",", matchingTags)}))";
                        }
                        else
                        {
                            searchedServiceSqls[sqlIndex] += $" AND (tag_id NOT IN ({string.Join(",", matchingTags)}) AND tag_id NOT IN ({string.Join(",", matchingTags)}))";
                        }
                    }
                }
            }

            string unionisedSql = string.Join(" UNION ", searchedServiceSqls);

            return unionisedSql;
        }

        /// <summary>
        /// Retrieves any parents and siblings of the tag (along with the original tag)
        /// </summary>
        /// <param name="searchedTags"></param>
        /// <returns></returns>
        private IEnumerable<int> GetMatchingTagIds(string tagName)
        {
            ITagRepository tagRepository = new DATagRepository();

            Tag matchingTag = tagRepository.GetTag(tagName);

            // If we can't find a matching tag, we won't be able to find any children
            if (matchingTag == null)
            {
                return new int[0];
            }

            IEnumerable<Tag> matchingChildren = tagRepository.GetTagChildren(matchingTag.TagId).ToList();

            // Search for any files which match the tag as a direct mapping, parent mapping or sibling mapping
            List<int> searchedTagIds = matchingChildren.Select(tag => tag.TagId).ToList();
            searchedTagIds.Add(matchingTag.TagId);

            return searchedTagIds;
        }
    }
}
