using DataAccessLayer;
using HydrusSharp.Core.Enums;
using HydrusSharp.Data.Models.Client;
using HydrusSharp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydrusSharp.Data.Repositories.DataAccess
{
    public class DAJsonDumpRepository : IJsonDumpRepository
    {
        public HashedJsonDump GetHashedJsonDump(string hash)
        {
            DbRow row;

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("Client")))
            {
                row = dbContext.GetDataRow("SELECT * FROM json_dumps_hashed WHERE LOWER(HEX(hash)) = @hash", new[] { DbContext.GenerateParameter("@hash", @hash) });
            }

            return row != null ? new HashedJsonDump
            {
                Dump = UTF8Encoding.UTF8.GetString((byte[])row.GetColumnValue("dump")),
                Version = Convert.ToInt32(row.GetColumnValue("version")),
                DumpType = Convert.ToInt32(row.GetColumnValue("dump_type")),
                Hash = (byte[])row.GetColumnValue("hash")

            } : null;
        }

        public IEnumerable<HashedJsonDump> GetHashedJsonDumps()
        {
            IEnumerable<DbRow> rows;

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("Client")))
            {
                rows = dbContext.GetDataRows("SELECT * FROM json_dumps_hashed");
            }

            return rows.Select(row => new HashedJsonDump
            {
                Dump = UTF8Encoding.UTF8.GetString((byte[])row.GetColumnValue("dump")),
                Version = Convert.ToInt32(row.GetColumnValue("version")),
                DumpType = Convert.ToInt32(row.GetColumnValue("dump_type")),
                Hash = (byte[])row.GetColumnValue("hash")
            });
        }

        public IEnumerable<JsonDump> GetJsonDumps()
        {
            IEnumerable<DbRow> rows;

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("Client")))
            {
                rows = dbContext.GetDataRows("SELECT * FROM json_dumps");
            }

            return rows.Select(row => new JsonDump
            {
                Dump = UTF8Encoding.UTF8.GetString((byte[])row.GetColumnValue("dump")),
                Version = Convert.ToInt32(row.GetColumnValue("version")),
                DumpTypeId = Convert.ToInt32(row.GetColumnValue("dump_type"))
            });
        }

        public IEnumerable<NamedJsonDump> GetSessions()
        {
            IEnumerable<DbRow> rows;
            string sql = $@"SELECT
                                dump,
                                version,
                                dump_type,
                                dump_name,
                                timestamp
                            FROM json_dumps_named
                            WHERE dump_type = {(int)SerialisableType.GuiSessionContainer}
                            ORDER BY dump_name != 'exit session' AND dump_name != 'last_session' DESC, dump_name ASC, timestamp DESC";

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("Client")))
            {
                rows = dbContext.GetDataRows(sql);
            }

            IEnumerable<NamedJsonDump> sessions = rows
                .Select(row => new NamedJsonDump
                {
                    Dump = UTF8Encoding.UTF8.GetString((byte[])row.GetColumnValue("dump")),
                    Version = Convert.ToInt32(row.GetColumnValue("version")),
                    DumpTypeId = Convert.ToInt32(row.GetColumnValue("dump_type")),
                    DumpName = Convert.ToString(row.GetColumnValue("dump_name")),
                    Timestamp = Convert.ToInt32(row.GetColumnValue("timestamp"))
                });

            return sessions
                .GroupBy(session => session.DumpName)
                .Select(group => group.First());
        }
    }
}
