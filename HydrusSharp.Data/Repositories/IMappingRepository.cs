using HydrusSharp.Data.Models.ClientMappings;
using HydrusSharp.Data.Models.ViewModels;
using System.Collections.Generic;

namespace HydrusSharp.Data.Repositories
{
    public interface IMappingRepository
    {
        /// <summary>
        /// Gets all of the mappings for the specified service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        IEnumerable<CurrentMapping> GetCurrentMappings(int serviceId);

        /// <summary>
        /// Gets any mappings which match the given criteria
        /// </summary>
        /// <param name="collect"></param>
        /// <param name="sort"></param>
        /// <param name="filters"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IEnumerable<CurrentMapping> GetMatchingMappings(MediaCollectViewModel collect, MediaSortViewModel sort, SearchPredicateViewModel[] filters, int skip, int take);

        /// <summary>
        /// Gets the number of mappings that match the criteria
        /// </summary>
        /// <param name="collect"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        int GetMappingsCount(MediaCollectViewModel collect, SearchPredicateViewModel[] filters);
    }
}
