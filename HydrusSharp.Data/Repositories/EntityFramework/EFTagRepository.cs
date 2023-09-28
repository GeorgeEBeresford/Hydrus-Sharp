//using HydrusSharp.Data.DbContexts;
//using HydrusSharp.Data.Models.Client;
//using HydrusSharp.Data.Models.ClientMaster;
//using HydrusSharp.Data.Repositories;
//using System.Collections.Generic;
//using System.Linq;

//namespace HydrusSharp.Data.EntityFramework.Repositories
//{
//    public class EFTagRepository : ITagRepository
//    {
//        private ClientMasterDbContext ClientMasterDbContext { get; set; }
//        private ClientDbContext ClientDbContext { get; set; }

//        public EFTagRepository(ClientMasterDbContext clientMasterDbContext, ClientDbContext clientDbContext)
//        {
//            ClientMasterDbContext = clientMasterDbContext;
//            ClientDbContext = clientDbContext;
//        }

//        public IEnumerable<TagParent> GetTagParents()
//        {
//            return ClientDbContext.TagParents;
//        }

//        public IEnumerable<Tag> GetTagChildren(int parentTagId)
//        {
//            IQueryable<TagParent> matchingParents = ClientDbContext.TagParents.Where(tagParent => tagParent.ParentTagId == parentTagId);
//            int[] matchingTagIds = matchingParents.Select(matchingParent => matchingParent.ChildTagId).ToArray();

//            return matchingTagIds.Select(matchingTagId => ClientMasterDbContext.Tags.First(tag => tag.TagId == matchingTagId));
//        }

//        public IEnumerable<Tag> GetTags()
//        {
//            return ClientMasterDbContext.Tags;
//        }

//        public Tag GetTag(string tagName)
//        {
//            if (!tagName.Contains(":"))
//            {
//                return GetTag("", tagName);
//            }

//            // If we're searching for files by tags, the first ":" will be for a tag namespace
//            string namespaceValue = tagName.Substring(0, tagName.IndexOf(":"));
//            string subtagValue = tagName.Substring(tagName.IndexOf(":") + 1);

//            return GetTag(namespaceValue, subtagValue);
//        }

//        public Tag GetTag(string namespaceName, string subtagName)
//        {
//            Tag value = GetTags().FirstOrDefault(tag => tag.Namespace.Value == namespaceName && tag.Subtag.Value == subtagName);
//            return value;
//        }

//        public IEnumerable<Subtag> GetSubtags()
//        {
//            return ClientMasterDbContext.Subtags;
//        }

//        public Subtag GetSubtag(string name)
//        {
//            Subtag value = GetSubtags().FirstOrDefault(subtag => subtag.Value == name);
//            return value;
//        }

//        public IEnumerable<Namespace> GetNamespaces()
//        {
//            return ClientMasterDbContext.Namespaces;
//        }

//        public Namespace GetNamespace(string name)
//        {
//            Namespace value = GetNamespaces().FirstOrDefault(namespaceItem => namespaceItem.Value == name);
//            return value;
//        }
//    }
//}