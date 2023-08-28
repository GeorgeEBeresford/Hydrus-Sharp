using HydrusSharp.DbContexts;
using HydrusSharp.Enums;
using HydrusSharp.Models.Client;
using HydrusSharp.Models.ViewModel;
using HydrusSharp.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HydrusSharp.Controllers
{
    public class ClientController : Controller
    {
        private ClientDbContext DbContext { get; set; }
        private OptionRepository OptionRepository { get; set; }
        private JsonDumpRepository JsonDumpRepository { get; set; }

        public ClientController()
        {
            DbContext = new ClientDbContext();
            OptionRepository = new OptionRepository(DbContext);
            JsonDumpRepository = new JsonDumpRepository(DbContext);
        }

        [HttpGet]
        [ActionName("JsonDumps")]
        public JsonResult GetJsonDumps()
        {
            IEnumerable<JsonDump> jsonDumps = JsonDumpRepository.GetJsonDumps();

            return Json(ResultViewModel.Success(jsonDumps), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("Sessions")]
        public JsonResult GetSessions()
        {
            IEnumerable<NamedJsonDump> sessions = JsonDumpRepository.GetNamedJsonDumps()
                .Where(jsonDump => jsonDump.DumpType == SerialisableType.GuiSessionContainer);

            IEnumerable<NamedJsonDump> orderedSessions = sessions
                .OrderByDescending(jsonDump => jsonDump.DumpName != "exit session" && jsonDump.DumpName != "last session")
                .ThenBy(jsonDump => jsonDump.DumpName)
                .ThenByDescending(jsonDump => jsonDump.Timestamp);

            IEnumerable<NamedJsonDump> latestSessions = orderedSessions
                .GroupBy(jsonDump => jsonDump.DumpName)
                .Select(grouping => grouping.First());

            return Json(ResultViewModel.Success(latestSessions), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("HashedJsonDumps")]
        public JsonResult GetHashedJsonDumps()
        {
            IEnumerable<HashedJsonDump> jsonDump = JsonDumpRepository.GetHashedJsonDumps();

            return Json(ResultViewModel.Success(jsonDump), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("HashedJsonDump")]
        public JsonResult GetHashedJsonDumps(string hash)
        {
            HashedJsonDump jsonDump = JsonDumpRepository.GetHashedJsonDump(hash);

            return Json(ResultViewModel.Success(jsonDump), JsonRequestBehavior.AllowGet);
        }
    }
}