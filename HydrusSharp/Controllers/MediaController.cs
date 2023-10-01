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

            IMappingRepository mappingRepository = new DAMappingRepository();

            // Downloads the file instead of setting the tab title. Leave as-is for now
            //string title = mappingRepository.GetFileTitle(hashId) ?? $"Media (Hash {hashId})";

            IDictionary<MimeType, string> mimeToContentTypes = new Dictionary<MimeType, string>
            {
                { MimeType.IMAGE_APNG, "image/apng" },
                { MimeType.IMAGE_BMP, "image/bmp" },
                { MimeType.IMAGE_GIF, "image/gif" },
                { MimeType.IMAGE_ICON, "image/vnd.microsoft.icon" },
                { MimeType.IMAGE_JPEG, "image/jpeg" },
                { MimeType.IMAGE_PNG, "image/png" },
                { MimeType.IMAGE_SVG, "image/svg+xml" },
                { MimeType.IMAGE_TIFF, "image/tiff" },
                { MimeType.IMAGE_WEBP, "image/webp" },
                { MimeType.UNDETERMINED_PNG, "image/png" },
                { MimeType.GENERAL_IMAGE, "image/jpeg" },
                { MimeType.VIDEO_AVI, "video/x-msvideo" },
                { MimeType.VIDEO_FLV, "video/x-flv" },
                { MimeType.VIDEO_MKV, "video/x-matroska" },
                { MimeType.VIDEO_MOV, "video/quicktime" },
                { MimeType.VIDEO_MP4, "video/mp4" },
                { MimeType.VIDEO_OGV, "video/ogg" },
                { MimeType.VIDEO_REALMEDIA, "application/vnd.rn-realmedia" },
                { MimeType.VIDEO_WEBM, "video/webm" },
                { MimeType.VIDEO_WMV, "video/x-ms-wmv" },
                { MimeType.GENERAL_VIDEO, "video/mp4" },
                { MimeType.UNDETERMINED_MP4, "video/mp4" }
            };

            string contentType = mimeToContentTypes[mimeType] ?? "octet/stream";

            return new FileStreamResult(matchingMedia.OpenRead(), contentType);
            //{
            //    FileDownloadName = title
            //};
        }
    }
}