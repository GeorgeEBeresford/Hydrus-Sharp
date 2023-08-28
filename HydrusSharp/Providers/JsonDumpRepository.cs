using HydrusSharp.DbContexts;
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

        public IEnumerable<NamedJsonDump> GetNamedJsonDumps()
        {
            IEnumerable<NamedJsonDump> jsonDumps = ClientDbContext.NamedJsonDumps;

            return jsonDumps;
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