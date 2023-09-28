using HydrusSharp.Core.Enums;
using HydrusSharp.Data.Models.ClientMaster;
using HydrusSharp.Data.Repositories;
using HydrusSharp.Data.Repositories.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace HydrusSharp.Controllers
{
    public class MediaController : Controller
    {
        public MediaController()
        {
        }

        [HttpGet]
        public ActionResult GetMedia(int hashId, bool isThumbnail)
        {
            IHashRepository hashRepository = new DAHashRepository();
            Hash matchingHash = hashRepository.GetById(hashId);

            DirectoryInfo clientFilesDirectory = new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["HydrusNetworkDB"], "client_files"));
            if (!clientFilesDirectory.Exists)
            {
                throw new InvalidOperationException("Could not find the client files directory");
            }

            IEnumerable<DirectoryInfo> subdirectories = clientFilesDirectory.EnumerateDirectories();
            IEnumerable<FileInfo> files = clientFilesDirectory.EnumerateFiles("*", SearchOption.AllDirectories);
            IEnumerable<FileInfo> matchingMedias = files.Where(media => media.Name.Substring(0, media.Name.Length - media.Extension.Length) == matchingHash.HashString);
            FileInfo matchingMedia = matchingMedias.FirstOrDefault(media => (media.Extension == ".thumbnail") == isThumbnail);


            if (matchingMedia == null)
            {
                return new HttpStatusCodeResult(404, "Media item not found");
            }

            IFileRepository fileRepository = new DAFileRepository();
            MimeType mimeType = fileRepository.GetMimeType(hashId);


            return new FileStreamResult(matchingMedia.OpenRead(), mimeType.ToString().Replace("_", "/"));
        }
    }
}