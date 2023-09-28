//using HydrusSharp.Core.Enums;
//using HydrusSharp.Data.DbContexts;
//using HydrusSharp.Data.Models.Client;
//using HydrusSharp.Data.Repositories;
//using System.Collections.Generic;
//using System.Linq;

//namespace HydrusSharp.Data.EntityFramework.Repositories
//{
//    public class EFJsonDumpRepository : IJsonDumpRepository
//    {
//        private ClientDbContext ClientDbContext { get; set; }

//        public EFJsonDumpRepository(ClientDbContext dbContext)
//        {
//            ClientDbContext = dbContext;
//        }

//        public IEnumerable<JsonDump> GetJsonDumps()
//        {
//            IQueryable<JsonDump> jsonDumps = ClientDbContext.JsonDumps;

//            return jsonDumps;
//        }

//        public IEnumerable<NamedJsonDump> GetSessions()
//        {
//            IQueryable<NamedJsonDump> sessions = ClientDbContext.NamedJsonDumps
//                .Where(jsonDump => jsonDump.DumpTypeId == (int)SerialisableType.GuiSessionContainer);

//            IEnumerable<NamedJsonDump> orderedSessions = sessions
//                .OrderByDescending(jsonDump => jsonDump.DumpName != "exit session" && jsonDump.DumpName != "last session")
//                .ThenBy(jsonDump => jsonDump.DumpName)
//                .ThenByDescending(jsonDump => jsonDump.Timestamp);

//            IEnumerable<NamedJsonDump> latestSessions = orderedSessions
//                .GroupBy(jsonDump => jsonDump.DumpName)
//                .Select(grouping => grouping.FirstOrDefault());

//            return latestSessions;
//        }

//        public IEnumerable<HashedJsonDump> GetHashedJsonDumps()
//        {
//            IQueryable<HashedJsonDump> jsonDump = ClientDbContext.HashedJsonDumps;

//            return jsonDump;
//        }

//        public HashedJsonDump GetHashedJsonDump(string hash)
//        {
//            HashedJsonDump jsonDump = ClientDbContext.HashedJsonDumps.AsEnumerable().FirstOrDefault(potentialJsonDump => potentialJsonDump.HashedString == hash);

//            return jsonDump;
//        }
//    }
//}