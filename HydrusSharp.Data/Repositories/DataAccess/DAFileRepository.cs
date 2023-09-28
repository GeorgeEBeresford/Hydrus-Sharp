using DataAccessLayer;
using HydrusSharp.Core.Enums;
using HydrusSharp.Data.Models.Client;
using HydrusSharp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HydrusSharp.Data.Repositories.DataAccess
{
    public class DAFileRepository : IFileRepository
    {
        public IEnumerable<FileInfo> GetFileInfos(IEnumerable<int> hashIds)
        {
            IEnumerable<DbRow> dbRows;
            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("Client")))
            {
                dbRows = dbContext.GetDataRows($"SELECT * FROM files_info WHERE hash_id IN ({string.Join(",", hashIds)})");
            }

            return dbRows.Select(row => new FileInfo
            {
                HashId = Convert.ToInt32(row.GetColumnValue("hash_id")),
                Size = Convert.ToInt64(row.GetColumnValue("size")),
                MimeType = (MimeType)Convert.ToInt32(row.GetColumnValue("mime")),
                Width = row.GetColumnValue("width") != DBNull.Value ? (int?)Convert.ToInt32(row.GetColumnValue("width")) : null,
                Height = row.GetColumnValue("height") != DBNull.Value ? (int?)Convert.ToInt32(row.GetColumnValue("height")) : null,
                Duration = row.GetColumnValue("duration") != DBNull.Value ? (int?)Convert.ToInt32(row.GetColumnValue("duration")) : null,
                FrameCount = row.GetColumnValue("num_frames") != DBNull.Value ? (int?)Convert.ToInt32(row.GetColumnValue("num_frames")) : null,
                HasAudio = Convert.ToBoolean(row.GetColumnValue("has_audio")),
                WordCount = row.GetColumnValue("num_words") != DBNull.Value ? (int?)Convert.ToInt32(row.GetColumnValue("num_words")) : null,
            });
        }

        public MimeType GetMimeType(int hashId)
        {
            DbRow dbRow;
            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("Client")))
            {
                dbRow = dbContext.GetDataRow($"SELECT mime FROM files_info WHERE hash_id = @hashId", new[] { DbContext.GenerateParameter("@hashId", hashId) });
            }

            return (MimeType)Convert.ToInt32(dbRow.GetColumnValue("mime"));
        }
    }
}
