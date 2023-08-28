using HydrusSharp.DbContexts;
using HydrusSharp.Models.Client;
using HydrusSharp.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace HydrusSharp.Tests.Repositories
{
    [TestClass]
    public class JsonDumpRepositoryTest
    {
        private ClientDbContext ClientDbContext { get; set; }
        private JsonDumpRepository JsonDumpService { get; set; }

        public JsonDumpRepositoryTest()
        {
            ClientDbContext = new ClientDbContext();
            JsonDumpService = new JsonDumpRepository(ClientDbContext);
        }

        [TestMethod]
        public void GetJsonDumps()
        {
            IEnumerable<JsonDump> jsonDumps = JsonDumpService.GetJsonDumps();
            Assert.IsNotNull(jsonDumps, "JSON dumps are null");
            Assert.AreNotEqual(0, jsonDumps.Count(), "No JSON dumps found");
        }
    }
}
