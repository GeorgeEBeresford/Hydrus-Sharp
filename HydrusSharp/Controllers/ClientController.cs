using HydrusSharp.Data.Models.Client;
using HydrusSharp.Data.Models.ClientMappings;
using HydrusSharp.Data.Models.ViewModels;
using HydrusSharp.Data.Repositories;
using HydrusSharp.Data.Repositories.DataAccess;
using HydrusSharp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HydrusSharp.Controllers
{
    public class ClientController : Controller
    {
        public ClientController()
        {
        }

        [HttpGet]
        [ActionName("JsonDumps")]
        public JsonResult GetJsonDumps()
        {
            IJsonDumpRepository jsonDumpRepository = new DAJsonDumpRepository();
            IEnumerable<JsonDump> jsonDumps = jsonDumpRepository.GetJsonDumps();

            return Json(ResultViewModel.Success(jsonDumps), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("Sessions")]
        public JsonResult GetSessions()
        {
            IJsonDumpRepository jsonDumpRepository = new DAJsonDumpRepository();
            IEnumerable<NamedJsonDump> sessions = jsonDumpRepository.GetSessions();

            return Json(ResultViewModel.Success(sessions), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("HashedJsonDumps")]
        public JsonResult GetHashedJsonDumps()
        {
            IJsonDumpRepository jsonDumpRepository = new DAJsonDumpRepository();
            IEnumerable<HashedJsonDump> jsonDump = jsonDumpRepository.GetHashedJsonDumps();

            return Json(ResultViewModel.Success(jsonDump), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("HashedJsonDump")]
        public JsonResult GetHashedJsonDumps(string hash)
        {
            IJsonDumpRepository jsonDumpRepository = new DAJsonDumpRepository();
            HashedJsonDump jsonDump = jsonDumpRepository.GetHashedJsonDump(hash);

            return Json(ResultViewModel.Success(jsonDump), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("MatchingFileInfo")]
        public JsonResult GetMatchingFileInfo(MediaCollectViewModel collect, MediaSortViewModel sort, SearchPredicateViewModel[] filters, int skip, int take)
        {
            IFileRepository fileRepository = new DAFileRepository();
            IMappingRepository mappingRepository = new DAMappingRepository();
            ITagRepository tagRepository = new DATagRepository();

            IEnumerable<CurrentMapping> matchingMappings = mappingRepository.GetMatchingMappings(collect, sort, filters, skip, take);
            IEnumerable<FileInfo> fileInfos = fileRepository.GetFileInfos(matchingMappings.Select(mapping => mapping.HashId));
            int totalCount = mappingRepository.GetMappingsCount(collect, filters);

            PaginatedResultViewModel paginatedResults = new PaginatedResultViewModel
            {
                Count = totalCount,
                Items = fileInfos.Select(fileInfo => new FileInfoViewModel
                {
                    HashId = fileInfo.HashId,
                    Size = fileInfo.Size,
                    Width = fileInfo.Width ?? 0,
                    Height = fileInfo.Height ?? 0,
                    MimeType = fileInfo.MimeType.ToString(),
                    Duration = fileInfo.Duration,
                    FrameCount = fileInfo.FrameCount,
                    HasAudio = fileInfo.HasAudio,
                    Tags = matchingMappings.First(mapping => mapping.HashId == fileInfo.HashId).Tags.ToArray()
                })
                .ToArray()
            };

            return Json(ResultViewModel.Success(paginatedResults));
        }

        [HttpGet]
        [ActionName("Option")]
        public JsonResult GetOption(string optionName)
        {
            IOptionRepository optionRepository = new DAOptionRepository();
            OptionCollection optionCollection = optionRepository.GetOptions();
            OptionService optionService = new OptionService();

            string value = optionService.GetValue(optionCollection, optionName);

            return Json(ResultViewModel.Success(value), JsonRequestBehavior.AllowGet);
        }
    }
}