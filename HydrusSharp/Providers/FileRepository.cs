using HydrusSharp.DbContexts;
using HydrusSharp.Models.Client;
using HydrusSharp.Models.ViewModel;
using System.Collections.Generic;

namespace HydrusSharp.Providers
{
    public class FileRepository
    {
        private ClientDbContext ClientDbContext { get; set; }

        public FileRepository(ClientDbContext dbContext)
        {
            ClientDbContext = dbContext;
        }

        public IEnumerable<FileInfo> GetFileInfos(MediaCollectViewModel collect, MediaSortViewModel sort, SearchPredicateViewModel[] filters)
        {
            return ClientDbContext.FileInfos;
        }
    }
}