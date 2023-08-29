using HydrusSharp.DbContexts;
using HydrusSharp.Models.ClientMaster;
using HydrusSharp.Providers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HydrusSharp.Controllers
{
    public class MediaController : Controller
    {
        private ClientDbContext ClientDbContext { get; set; }
        private ClientMasterDbContext ClientMasterDbContext { get; set; }

        public MediaController()
        {
            ClientDbContext = new ClientDbContext();
            ClientMasterDbContext = new ClientMasterDbContext();
        }

        [HttpGet]
        public FileStreamResult GetMedia(int hashId, bool isThumbnail)
        {
            HashRepository hashRepository = new HashRepository(ClientMasterDbContext);
            Hash matchingHash = hashRepository.GetById(hashId);

            DirectoryInfo clientFilesDirectory = new DirectoryInfo(ConfigurationManager.AppSettings["ClientFilesLocation"]);
            if (!clientFilesDirectory.Exists)
            {
                throw new InvalidOperationException("Could not find the client files directory");
            }

            IEnumerable<DirectoryInfo> subdirectories = clientFilesDirectory.EnumerateDirectories();
            IEnumerable<FileInfo> files = clientFilesDirectory.EnumerateFiles("*", SearchOption.AllDirectories);
            IEnumerable<FileInfo> matchingMedias = files.Where(media => media.Name.Substring(0, media.Name.Length - media.Extension.Length) == matchingHash.HashString);
            FileInfo matchingMedia = matchingMedias.First(media => (media.Extension == ".thumbnail") == isThumbnail);

            return new FileStreamResult(matchingMedia.OpenRead(), "application/octet-stream");
        }
    }
}