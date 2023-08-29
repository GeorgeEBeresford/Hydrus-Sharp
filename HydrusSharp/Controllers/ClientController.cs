using HydrusSharp.DbContexts;
using HydrusSharp.Enums;
using HydrusSharp.Models.Client;
using HydrusSharp.Models.ViewModel;
using HydrusSharp.Providers;
using HydrusSharp.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HydrusSharp.Controllers
{
    public class ClientController : Controller
    {
        private ClientDbContext DbContext { get; set; }

        public ClientController()
        {
            DbContext = new ClientDbContext();
        }

        [HttpGet]
        [ActionName("JsonDumps")]
        public JsonResult GetJsonDumps()
        {
            JsonDumpRepository jsonDumpRepository = new JsonDumpRepository(DbContext);
            IEnumerable<JsonDump> jsonDumps = jsonDumpRepository.GetJsonDumps();

            return Json(ResultViewModel.Success(jsonDumps), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("Sessions")]
        public JsonResult GetSessions()
        {
            JsonDumpRepository jsonDumpRepository = new JsonDumpRepository(DbContext);
            IEnumerable<NamedJsonDump> sessions = jsonDumpRepository.GetSessions();

            return Json(ResultViewModel.Success(sessions), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("HashedJsonDumps")]
        public JsonResult GetHashedJsonDumps()
        {
            JsonDumpRepository jsonDumpRepository = new JsonDumpRepository(DbContext);
            IEnumerable<HashedJsonDump> jsonDump = jsonDumpRepository.GetHashedJsonDumps();

            return Json(ResultViewModel.Success(jsonDump), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("HashedJsonDump")]
        public JsonResult GetHashedJsonDumps(string hash)
        {
            JsonDumpRepository jsonDumpRepository = new JsonDumpRepository(DbContext);
            HashedJsonDump jsonDump = jsonDumpRepository.GetHashedJsonDump(hash);

            return Json(ResultViewModel.Success(jsonDump), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("MatchingFileInfo")]
        public JsonResult GetMatchingFileInfo(MediaCollectViewModel collect, MediaSortViewModel sort, SearchPredicateViewModel[] filters, int? skip, int? take)
        {
            FileRepository fileRepository = new FileRepository(DbContext);
            IEnumerable<FileInfo> fileInfos = fileRepository.GetFileInfos(collect, sort, filters);

            if (skip.HasValue)
            {
                fileInfos = fileInfos.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                fileInfos = fileInfos.Take(take.Value);
            }

            return Json(ResultViewModel.Success(fileInfos));
        }

        [HttpGet]
        [ActionName("Option")]
        public JsonResult GetOption(string optionName)
        {
            OptionRepository optionRepository = new OptionRepository(DbContext);
            return Json(ResultViewModel.Success(optionRepository.GetOption(optionName)), JsonRequestBehavior.AllowGet);
        }
    }
}