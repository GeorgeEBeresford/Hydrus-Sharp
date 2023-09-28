//using HydrusSharp.Data.DbContexts;
//using HydrusSharp.Data.Models.Client;
//using HydrusSharp.Data.Repositories;
//using System.Linq;

//namespace HydrusSharp.Data.EntityFramework.Repositories
//{
//    public class EFOptionRepository : IOptionRepository
//    {
//        private ClientDbContext ClientDbContext { get; set; }

//        public EFOptionRepository(ClientDbContext dbContext) { 

//            ClientDbContext = dbContext;
//        }

//        public OptionCollection GetOptions()
//        {
//            OptionCollection collection = ClientDbContext.Options.First();
//            return collection;
//        }
//    }
//}