using HydrusSharp.DbContexts;
using HydrusSharp.Models.Client;
using HydrusSharp.Models.ClientMaster;
using System.Collections.Generic;
using System.Linq;

namespace HydrusSharp.Providers
{
    public class TagRepository
    {
        private ClientMasterDbContext ClientMasterDbContext { get; set; }
        private ClientDbContext ClientDbContext { get; set; }

        public TagRepository(ClientMasterDbContext clientMasterDbContext, ClientDbContext clientDbContext)
        {
            ClientMasterDbContext = clientMasterDbContext;
            ClientDbContext = clientDbContext;
        }

        public IEnumerable<TagParent> GetTagParents()
        {
            return ClientDbContext.TagParents;
        }

        public IEnumerable<Tag> GetTagChildren(int parentTagId)
        {
            IQueryable<TagParent> matchingParents = ClientDbContext.TagParents.Where(tagParent => tagParent.ParentTagId == parentTagId);
            int[] matchingTagIds = matchingParents.Select(matchingParent => matchingParent.ChildTagId).ToArray();

            return matchingTagIds.Select(matchingTagId => ClientMasterDbContext.Tags.First(tag => tag.TagId == matchingTagId));
        }

        public IEnumerable<Tag> GetTags()
        {
            return ClientMasterDbContext.Tags;
        }

        public Tag GetTag(string tagName)
        {
            if (!tagName.Contains(":"))
            {
                return GetTag("", tagName);
            }

            // If we're searching for files by tags, the first ":" will be for a tag namespace
            string namespaceValue = tagName.Substring(0, tagName.IndexOf(":"));
            string subtagValue = tagName.Substring(tagName.IndexOf(":") + 1);

            return GetTag(namespaceValue, subtagValue);
        }

        public Tag GetTag(string namespaceName, string subtagName)
        {
            return GetTags().FirstOrDefault(tag => tag.Namespace.Value == namespaceName && tag.Subtag.Value == subtagName);
        }

        public IEnumerable<Subtag> GetSubtags()
        {
            return ClientMasterDbContext.Subtags;
        }

        public Subtag GetSubtag(string name)
        {
            return GetSubtags().FirstOrDefault(subtag => subtag.Value == name);
        }

        public IEnumerable<Namespace> GetNamespaces()
        {
            return ClientMasterDbContext.Namespaces;
        }

        public Namespace GetNamespace(string name)
        {
            return GetNamespaces().FirstOrDefault(namespaceItem => namespaceItem.Value == name);
        }
    }
}