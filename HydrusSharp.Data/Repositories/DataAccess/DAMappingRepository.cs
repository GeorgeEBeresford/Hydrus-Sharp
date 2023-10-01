using DataAccessLayer;
using HydrusSharp.Core.Enums;
using HydrusSharp.Data.Models.ClientMappings;
using HydrusSharp.Data.Models.ClientMaster;
using HydrusSharp.Data.Models.ViewModels;
using HydrusSharp.Data.Services;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public int GetMappingsCount(MediaCollectViewModel collect, SearchPredicateViewModel[] filters)
        {
            string mappingsSql = $@"{PrepareAttachmentSql()}
                                    {PrepareCommonTableExpression(false, collect?.Namespaces)}
                                    ,countable_results AS (
                                        SELECT *
                                        FROM current_mappings
                                        INNER JOIN filterable_tags ON filterable_tags.hash_id = current_mappings.hash_id
                                        {(collect.Namespaces != null && collect.Namespaces.Any() ? "INNER JOIN groupable_tags ON groupable_tags.hash_id = current_mappings.hash_id" : "")}
                                        {GenerateFilteringSql(filters)}
                                        GROUP BY current_mappings.hash_id
                                    )
                                    SELECT COUNT(*) AS TotalItems FROM countable_results
                                    {PrepareDetachmentSql()}";

            DbRow mappingRow;
            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMappings")))
            {
                try
                {
                    mappingRow = dbContext.GetDataRow(mappingsSql);
                }
                catch (SqliteException)
                {
                    // We don't want to leave the database in a "in use" state
                    dbContext.GetDataRow(PrepareDetachmentSql());
                    throw;
                }
            }

            return mappingRow != null ? Convert.ToInt32(mappingRow.GetColumnValue("TotalItems")) : 0;
        }

        public IEnumerable<CurrentMapping> GetMatchingMappings(MediaCollectViewModel collect, MediaSortViewModel sort, SearchPredicateViewModel[] filters, int skip, int take)
        {
            string mappingsSql =
                $@"{PrepareAttachmentSql()}
                  {PrepareCommonTableExpression(false, collect.Namespaces)}
                  SELECT current_mappings.*, filterable_tags.flattened_tags_friendly
                  FROM current_mappings
                  INNER JOIN client.files_info ON files_info.hash_id = current_mappings.hash_id
                  INNER JOIN filterable_tags ON filterable_tags.hash_id = current_mappings.hash_id
                  {(collect.Namespaces != null && collect.Namespaces.Any() ? "INNER JOIN groupable_tags ON groupable_tags.hash_id = current_mappings.hash_id" : "")}
                  {GenerateFilteringSql(filters)}
                  GROUP BY current_mappings.hash_id
                  {GenerateSortingSql(sort)}
                  LIMIT {take} OFFSET {skip}
                  {PrepareDetachmentSql()}";

            IEnumerable<DbRow> mappingRows;
            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMappings")))
            {
                try
                {
                    mappingRows = dbContext.GetDataRows(mappingsSql);
                }
                catch (SqliteException)
                {
                    // We don't want to leave the database in a "in use" state
                    dbContext.GetDataRow(PrepareDetachmentSql());
                    throw;
                }
            }

            return mappingRows
                .Select(row => new CurrentMapping
                {
                    HashId = Convert.ToInt32(row.GetColumnValue("hash_id")),
                    TagId = Convert.ToInt32(row.GetColumnValue("tag_id")),
                    Tags = Convert.ToString(row.GetColumnValue("flattened_tags_friendly")).Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries),
                });
        }

        private string PrepareAttachmentSql()
        {
            string attachmentSql = $@"; ATTACH DATABASE '{ConfigurationManager.AppSettings["HydrusNetworkDB"]}\\client.db' AS 'client'
                                      ; ATTACH DATABASE '{ConfigurationManager.AppSettings["HydrusNetworkDB"]}\\client.master.db' AS 'client.master'";

            return attachmentSql;
        }

        private string PrepareDetachmentSql()
        {
            string detachmentSql = $@"; DETACH DATABASE 'client'
                                      ; DETACH DATABASE 'client.master'";

            return detachmentSql;
        }

        private string GenerateSortingSql(MediaSortViewModel sort)
        {
            switch (sort.SystemSortType)
            {
                case SortFilesBy.SORT_FILES_BY_NUM_PIXELS:

                    return " ORDER BY files_info.width * files_info.height DESC";

                default:

                    return "";
            }
        }

        private string PrepareCommonTableExpression(bool includeDeletedFiles, IEnumerable<string> groupByNamespace)
        {
            // Todo - Filters should eventually specify which services to retrieve from
            List<int> searchedFileServices = new List<int> {
                6 // my files service
            };
            int[] searchedTagServices = new[] {
                9, // local tags
                10 // downloader tags
            };

            if (includeDeletedFiles)
            {
                searchedFileServices.Add(3); // all deleted files service
            }

            IEnumerable<string> searchedCurrentFilesSqls = searchedFileServices.Select(serviceId => $"SELECT current_files_{serviceId}.hash_id, {serviceId} AS file_service_id FROM current_files_{serviceId}");

            // Filter out any deleted files
            IEnumerable<string> searchedMappingsSqls = searchedTagServices.Select(serviceId => $"SELECT current_mappings_{serviceId}.hash_id, current_mappings_{serviceId}.tag_id, {serviceId} AS tag_service_id FROM current_mappings_{serviceId} INNER JOIN current_files ON current_mappings_{serviceId}.hash_id = current_files.hash_id");

            string sql = $@"; WITH
                                current_files AS (
                                    {string.Join(" UNION ", searchedCurrentFilesSqls)}
                                ),
                                current_mappings AS (
                                    {string.Join(" UNION ", searchedMappingsSqls)}
                                ),
                                tags_with_parents AS (
	                                SELECT
		                                tags.tag_id,
		                                tag_parents.parent_tag_id
	                                FROM tags
	                                LEFT JOIN tag_parents ON tags.tag_id = tag_parents.child_tag_id
	                                WHERE tag_parents.status IS NULL OR tag_parents.status == 0
                                ),
                                tags_with_ancestors AS (
	                                SELECT
		                                tag_id,
		                                parent_tag_id,
		                                coalesce(tags_with_parents.parent_tag_id, tags_with_parents.tag_id) AS any_tag_id
	                                FROM tags_with_parents
	                                UNION ALL
	                                SELECT
		                                tags_with_parents.tag_id,
		                                tags_with_ancestors.parent_tag_id,
		                                coalesce(tags_with_ancestors.parent_tag_id, tags_with_parents.tag_id) AS any_tag_id
	                                FROM tags_with_ancestors
	                                JOIN tags_with_parents ON tags_with_ancestors.tag_id = tags_with_parents.parent_tag_id
                                )
                                ,filterable_tags AS (
	                                SELECT
		                                current_mappings.hash_id,
		                                '|' || GROUP_CONCAT(tags_with_ancestors.any_tag_id , '|') || '|' as flattened_tags,
		                                '|' || GROUP_CONCAT(CASE WHEN namespaces.namespace != '' THEN namespaces.namespace || ':' || subtags.subtag ELSE subtags.subtag END, '|') || '|' as flattened_tags_friendly
	                                FROM current_mappings
	                                INNER JOIN tags_with_ancestors ON tags_with_ancestors.tag_id = current_mappings.tag_id
									INNER JOIN tags ON tags.tag_id = tags_with_ancestors.any_tag_id
									LEFT JOIN namespaces ON namespaces.namespace_id = tags.namespace_id
									LEFT JOIN subtags ON subtags.subtag_id = tags.subtag_id
	                                GROUP BY current_mappings.hash_id
                                )";

            if (groupByNamespace != null && groupByNamespace.Any())
            {
                sql += $@",groupable_tags AS (
	                        SELECT hash_id
	                        FROM (
		                        select
			                        current_mappings.hash_id
			                        ,'|' || GROUP_CONCAT(tags_with_ancestors.any_tag_id , '|') || '|' as flattened_tags
		                        from current_mappings
		                        INNER JOIN tags_with_ancestors ON tags_with_ancestors.tag_id = current_mappings.tag_id
		                        INNER JOIN tags ON tags.tag_id = tags_with_ancestors.any_tag_id
		                        INNER JOIN namespaces ON namespaces.namespace_id = tags.namespace_id
		                        INNER JOIN subtags ON subtags.subtag_id = tags.subtag_id
		                        WHERE namespaces.namespace IN ('{string.Join("', '", groupByNamespace)}')
		                        GROUP BY current_mappings.hash_id
	                        )
	                        GROUP BY flattened_tags
                        )";
            }

            return sql;
        }

        private string GenerateFilteringSql(SearchPredicateViewModel[] filters)
        {
            // If they haven't requested any filters (even system:everything) then return no files
            if (filters == null)
            {
                return " WHERE 1=2";
            }

            // We want to return mappings from all of the requested services
            string sql = " WHERE 1=1";

            foreach (SearchPredicateViewModel filter in filters)
            {
                if (filter.SearchType == PredicateType.PREDICATE_TYPE_TAG)
                {
                    ITagRepository tagRepository = new DATagRepository();
                    Tag matchingTag = tagRepository.GetTag(filter.SearchData[0] as string);

                    if (matchingTag == null)
                    {
                        // We couldn't actually find the tag we're looking for. Logically, there can be no mappings that match a non-existent tag
                        sql += " AND 1=2";
                    }
                    else if (filter.MustBeTrue)
                    {
                        sql += $" AND flattened_tags LIKE '%|{matchingTag.TagId}|%'";
                    }
                    else
                    {
                        sql += $" AND flattened_tags NOT LIKE '%|{matchingTag.TagId}|%'";
                    }
                }
            }

            return sql;
        }
    }
}
