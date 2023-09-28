using DataAccessLayer;
using HydrusSharp.Data.Models.Client;
using HydrusSharp.Data.Models.ClientMaster;
using HydrusSharp.Data.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace HydrusSharp.Data.Repositories.DataAccess
{
    public class DATagRepository : ITagRepository
    {
        public Namespace GetNamespace(string name)
        {
            DbRow row;

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMaster")))
            {
                row = dbContext.GetDataRow("SELECT * FROM namespaces WHERE namespace = @name", new[] { DbContext.GenerateParameter("@name", name) });
            }

            return row != null ? new Namespace
            {
                NamespaceId = Convert.ToInt32(row.GetColumnValue("namespace_id")),
                Value = Convert.ToString(row.GetColumnValue("namespace"))
            }
            : null;
        }

        public IEnumerable<Namespace> GetNamespaces()
        {
            IEnumerable<DbRow> rows;

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMaster")))
            {
                rows = dbContext.GetDataRows("SELECT * FROM namespaces");
            }

            return rows.Select(row => new Namespace
            {
                NamespaceId = Convert.ToInt32(row.GetColumnValue("namespace_id")),
                Value = Convert.ToString(row.GetColumnValue("namespace"))
            });
        }

        public Subtag GetSubtag(string name)
        {
            DbRow row;

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMaster")))
            {
                row = dbContext.GetDataRow("SELECT * FROM subtags WHERE subtag = @name", new[] { DbContext.GenerateParameter("@name", name) });
            }

            return row != null ? new Subtag
            {
                SubtagId = Convert.ToInt32(row.GetColumnValue("subtag_id")),
                Value = Convert.ToString(row.GetColumnValue("subtag"))
            }
            : null;
        }

        public IEnumerable<Subtag> GetSubtags()
        {
            IEnumerable<DbRow> rows;

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMaster")))
            {
                rows = dbContext.GetDataRows("SELECT * FROM subtags");
            }

            return rows.Select(row => new Subtag
            {
                SubtagId = Convert.ToInt32(row.GetColumnValue("subtag_id")),
                Value = Convert.ToString(row.GetColumnValue("subtag"))
            });
        }

        public Tag GetTag(string tagName)
        {
            if (!tagName.Contains(":"))
            {
                return GetTag("", tagName);
            }

            // If we're searching for files by tags, the first ":" will be for a tag namespace
            string namespaceValue = tagName.Substring(0, tagName.IndexOf(":"));
            string subtagValue = tagName.Substring(tagName.IndexOf(":") + 1);

            return GetTag(namespaceValue, subtagValue);
        }

        public Tag GetTag(string namespaceName, string subtagName)
        {
            DbRow row;
            string sql = @"SELECT tags.* FROM tags
                            INNER JOIN namespaces ON namespaces.namespace_id = tags.namespace_id
                            INNER JOIN subtags ON subtags.subtag_id = tags.subtag_id
                            WHERE subtags.subtag = @subtagName AND namespaces.namespace = @namespaceName";

            DbParameter[] parameters = new[]
            {
                DbContext.GenerateParameter("@namespaceName", namespaceName),
                DbContext.GenerateParameter("@subtagName", subtagName)
            };

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMaster")))
            {
                row = dbContext.GetDataRow(sql, parameters);
            }

            return row != null ? new Tag
            {
                TagId = Convert.ToInt32(row.GetColumnValue("tag_id")),
                NamespaceId = Convert.ToInt32(row.GetColumnValue("namespace_id")),
                SubtagId = Convert.ToInt32(row.GetColumnValue("subtag_id"))
            }
            : null;
        }

        public IEnumerable<Tag> GetTagChildren(int parentTagId)
        {
            IEnumerable<DbRow> childTagRows;

            using (DbContext clientDbContext = new DbContext(ConnectionStringService.GetConnectionString("Client")))
            {
                childTagRows = clientDbContext.GetDataRows("SELECT child_tag_id FROM tag_parents WHERE parent_tag_id = @parentTagId AND status = 0", new[] { DbContext.GenerateParameter("@parentTagId", parentTagId) });
            }

            IEnumerable<int> childTagIds = childTagRows.Select(row => Convert.ToInt32(row.GetColumnValue("child_tag_id")));

            IEnumerable<DbRow> tagRows;
            string sql = $"SELECT * FROM tags WHERE tag_id IN ({string.Join(",", childTagIds)})";

            using (DbContext clientDbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMaster")))
            {
                tagRows = clientDbContext.GetDataRows(sql);
            }

            return tagRows.Select(row => new Tag
            {
                TagId = Convert.ToInt32(row.GetColumnValue("tag_id")),
                NamespaceId = Convert.ToInt32(row.GetColumnValue("namespace_id")),
                SubtagId = Convert.ToInt32(row.GetColumnValue("subtag_id"))
            });
        }

        public IEnumerable<TagParent> GetTagParents()
        {
            IEnumerable<DbRow> rows;

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("Client")))
            {
                rows = dbContext.GetDataRows("SELECT * FROM tag_parents");
            }

            return rows.Select(row => new TagParent
            {
                ParentTagId = Convert.ToInt32(row.GetColumnValue("parent_tag_id")),
                ChildTagId = Convert.ToInt32(row.GetColumnValue("child_tag_id"))
            });
        }

        public IEnumerable<Tag> GetTags()
        {
            IEnumerable<DbRow> rows;

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMaster")))
            {
                rows = dbContext.GetDataRows("SELECT * FROM tags");
            }

            return rows.Select(row => new Tag
            {
                TagId = Convert.ToInt32(row.GetColumnValue("tag_id")),
                NamespaceId = Convert.ToInt32(row.GetColumnValue("namespace_id")),
                SubtagId = Convert.ToInt32(row.GetColumnValue("subtag_id"))
            });
        }
    }
}
