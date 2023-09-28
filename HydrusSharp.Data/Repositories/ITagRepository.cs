using HydrusSharp.Data.Models.Client;
using HydrusSharp.Data.Models.ClientMaster;
using System.Collections.Generic;

namespace HydrusSharp.Data.Repositories
{
    public interface ITagRepository
    {
        /// <summary>
        /// Retrieves all of the parents of any tags
        /// </summary>
        /// <returns></returns>
        IEnumerable<TagParent> GetTagParents();

        /// <summary>
        /// Retrieves all of the children of the specified tag
        /// </summary>
        /// <param name="parentTagId"></param>
        /// <returns></returns>
        IEnumerable<Tag> GetTagChildren(int parentTagId);

        /// <summary>
        /// Retrieves all of the tags
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tag> GetTags();

        /// <summary>
        /// Retrieves all of the tags with the specified name
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        Tag GetTag(string tagName);

        /// <summary>
        /// Retrieves all of the tags with the given namespace and subtag name
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <param name="subtagName"></param>
        /// <returns></returns>
        Tag GetTag(string namespaceName, string subtagName);

        /// <summary>
        /// Retrieves all of the subtags
        /// </summary>
        /// <returns></returns>
        IEnumerable<Subtag> GetSubtags();

        /// <summary>
        /// Retrieves the subtag with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Subtag GetSubtag(string name);

        /// <summary>
        /// Retrieves all of the namespaces
        /// </summary>
        /// <returns></returns>
        IEnumerable<Namespace> GetNamespaces();

        /// <summary>
        /// Retrieves the namespace with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Namespace GetNamespace(string name);
    }
}
