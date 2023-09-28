using HydrusSharp.Core.Enums;
using HydrusSharp.Data.Models.Client;
using HydrusSharp.Data.Models.ViewModels;
using System.Collections.Generic;

namespace HydrusSharp.Data.Repositories
{
    /// <summary>
    /// Retrieves data relating to saved files
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// Gets the information about any files that match the hash ids
        /// </summary>
        /// <param name="hashIds"></param>
        /// <returns></returns>
        IEnumerable<FileInfo> GetFileInfos(IEnumerable<int> hashIds);

        /// <summary>
        /// Gets the mime type of the file with the specified ID
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        MimeType GetMimeType(int hashId);
    }
}
