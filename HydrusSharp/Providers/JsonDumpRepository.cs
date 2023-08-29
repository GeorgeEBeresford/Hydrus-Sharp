using HydrusSharp.DbContexts;
using HydrusSharp.Enums;
using HydrusSharp.Models.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace HydrusSharp.Repositories
{
    public class JsonDumpRepository
    {
        private ClientDbContext ClientDbContext { get; set; }

        public JsonDumpRepository(ClientDbContext dbContext)
        {
            ClientDbContext = dbContext;
        }

        [TestMethod]
        public IEnumerable<JsonDump> GetJsonDumps()
        {
            IEnumerable<JsonDump> jsonDumps = ClientDbContext.JsonDumps;

            return jsonDumps;
        }

        public IEnumerable<NamedJsonDump> GetSessions()
        {
            IEnumerable<NamedJsonDump> sessions = ClientDbContext.NamedJsonDumps
                .Where(jsonDump => jsonDump.DumpTypeId == (int)SerialisableType.GuiSessionContainer);

            IEnumerable<NamedJsonDump> orderedSessions = sessions
                .OrderByDescending(jsonDump => jsonDump.DumpName != "exit session" && jsonDump.DumpName != "last session")
                .ThenBy(jsonDump => jsonDump.DumpName)
                .ThenByDescending(jsonDump => jsonDump.Timestamp);

            IEnumerable<NamedJsonDump> latestSessions = orderedSessions
                .GroupBy(jsonDump => jsonDump.DumpName)
                .Select(grouping => grouping.First());

            return latestSessions;
        }

        public IEnumerable<HashedJsonDump> GetHashedJsonDumps()
        {
            IEnumerable<HashedJsonDump> jsonDump = ClientDbContext.HashedJsonDumps;

            return jsonDump;
        }

        public HashedJsonDump GetHashedJsonDump(string hash)
        {
            HashedJsonDump jsonDump = ClientDbContext.HashedJsonDumps.AsEnumerable().FirstOrDefault(potentialJsonDump => potentialJsonDump.HashedString == hash);

            return jsonDump;
        }
    }
}