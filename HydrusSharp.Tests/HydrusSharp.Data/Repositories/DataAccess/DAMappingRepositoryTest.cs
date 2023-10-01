using HydrusSharp.Core.Enums;
using HydrusSharp.Data.Models.ClientMappings;
using HydrusSharp.Data.Models.ViewModels;
using HydrusSharp.Data.Repositories.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace HydrusSharp.Tests.HydrusSharp.Data.Repositories.DataAccess
{
    [TestClass]
    public class DAMappingRepositoryTest
    {
        private DAMappingRepository MappingRepository { get; set; }

        public DAMappingRepositoryTest()
        {
            MappingRepository = new DAMappingRepository();
        }

        [TestMethod]
        public void GetMappingsWithNoFilter()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            MediaSortViewModel sortBy = new MediaSortViewModel { Namespaces = new[] { "title" } };
            SearchPredicateViewModel[] filterBy = null;

            // No filters should return no results
            IEnumerable<CurrentMapping> noFilterMappings = MappingRepository.GetMatchingMappings(collectBy, sortBy, filterBy, 0, 100);

            Assert.IsNotNull(noFilterMappings);
            Assert.AreEqual(0, noFilterMappings.Count());
        }

        [TestMethod]
        public void GetMappingsWithNonExistentTags()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            MediaSortViewModel sortBy = new MediaSortViewModel { Namespaces = new[] { "title" } };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "werjfuwfwuiefhiu" } } };

            // Filters with non-existent tags should not return results
            IEnumerable<CurrentMapping> notFoundMappings = MappingRepository.GetMatchingMappings(collectBy, sortBy, filterBy, 0, 100);

            Assert.IsNotNull(notFoundMappings);
            Assert.AreEqual(0, notFoundMappings.Count());
        }

        [TestMethod]
        public void GetMappingsWithNamespacedTag()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            MediaSortViewModel sortBy = new MediaSortViewModel { Namespaces = new[] { "title" } };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:white image" } } };

            // Search for tags with namespaces
            IEnumerable<CurrentMapping> titleMappings = MappingRepository.GetMatchingMappings(collectBy, sortBy, filterBy, 0, 100);

            Assert.IsNotNull(titleMappings);
            Assert.AreEqual(3, titleMappings.Count());
        }

        [TestMethod]
        public void GetMappingsWithNonNamespacedTag()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            MediaSortViewModel sortBy = new MediaSortViewModel { Namespaces = new[] { "title" } };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "memes" } } };

            // Search for tags without namespaces
            IEnumerable<CurrentMapping> skippedMappings = MappingRepository.GetMatchingMappings(collectBy, sortBy, filterBy, 0, 100);

            Assert.IsNotNull(skippedMappings);
            Assert.AreEqual(1, skippedMappings.Count());
        }

        [TestMethod]
        public void GetMappingsWithLimitedResults()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            MediaSortViewModel sortBy = new MediaSortViewModel { Namespaces = new[] { "title" } };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:white image" } } };

            // Specify an amount of results to return
            IEnumerable<CurrentMapping> takenMappings = MappingRepository.GetMatchingMappings(collectBy, sortBy, filterBy, 0, 1);

            Assert.IsNotNull(takenMappings);
            Assert.AreEqual(1, takenMappings.Count());
        }

        [TestMethod]
        public void GetMappingsWithParentTags()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            MediaSortViewModel sortBy = new MediaSortViewModel { Namespaces = new[] { "title" } };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "memes" } } };

            // Search for tags that are parents of other tags
            IEnumerable<CurrentMapping> parentTagMappings = MappingRepository.GetMatchingMappings(collectBy, sortBy, filterBy, 0, 100);

            Assert.IsNotNull(parentTagMappings);
            Assert.AreEqual(1, parentTagMappings.Count());
        }

        [TestMethod]
        public void GetMappingsWithAncestorTags()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            MediaSortViewModel sortBy = new MediaSortViewModel { Namespaces = new[] { "title" } };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "internet stuff" } } };

            // Search for tags that are parents of other parent tags
            IEnumerable<CurrentMapping> ancestorTagMappings = MappingRepository.GetMatchingMappings(collectBy, sortBy, filterBy, 0, 100);

            Assert.IsNotNull(ancestorTagMappings);
            Assert.AreEqual(1, ancestorTagMappings.Count());
        }

        [TestMethod]
        public void GetMappingsSortedByNumberOfPixels()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            MediaSortViewModel sortBy = new MediaSortViewModel { SystemSortType = SortFilesBy.SORT_FILES_BY_NUM_PIXELS };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:white image" } } };

            // Sort the results by the number of pixels
            IEnumerable<CurrentMapping> sortedByPixelsMappings = MappingRepository.GetMatchingMappings(collectBy, sortBy, filterBy, 0, 100);

            Assert.IsNotNull(sortedByPixelsMappings);
            Assert.AreEqual(3, sortedByPixelsMappings.Count());
        }

        [TestMethod]
        public void GetMappingsCollectedBySingleNamespace()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new[] { "title" } };
            MediaSortViewModel sortBy = new MediaSortViewModel { SystemSortType = SortFilesBy.SORT_FILES_BY_NUM_PIXELS };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:white image" } } };

            // Collect the results into groups of titles
            IEnumerable<CurrentMapping> collectedByTitleMappings = MappingRepository.GetMatchingMappings(collectBy, sortBy, filterBy, 0, 100);

            Assert.IsNotNull(collectedByTitleMappings);
            Assert.AreEqual(1, collectedByTitleMappings.Count());
        }

        [TestMethod]
        public void GetMappingsCollectedByMultipleNamespaces()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new[] { "title", "series" } };
            MediaSortViewModel sortBy = new MediaSortViewModel { SystemSortType = SortFilesBy.SORT_FILES_BY_NUM_PIXELS };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:white image" } } };

            // Collect the results into groups of titles and series
            IEnumerable<CurrentMapping> collectedByTitleAndSeriesMappings = MappingRepository.GetMatchingMappings(collectBy, sortBy, filterBy, 0, 100);

            Assert.IsNotNull(collectedByTitleAndSeriesMappings);
            Assert.AreEqual(2, collectedByTitleAndSeriesMappings.Count());
        }

        [TestMethod]
        public void GetCountWithNoFilter()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            SearchPredicateViewModel[] filterBy = null;

            // Filters with no tags should return no items
            int noFilterCount = MappingRepository.GetMappingsCount(collectBy, filterBy);

            Assert.AreEqual(0, noFilterCount);
        }

        [TestMethod]
        public void GetCountWithNonExistentTags()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "werjfuwfwuiefhiu" } } };

            // Filters with no tags should return no items
            int notFoundCount = MappingRepository.GetMappingsCount(collectBy, filterBy);

            Assert.AreEqual(0, notFoundCount);
        }

        [TestMethod]
        public void GetCountWithNamespacedTag()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:have a relaxing kappachino" } } };

            // Filter by a tag that has a namespace
            int titleCount = MappingRepository.GetMappingsCount(collectBy, filterBy);

            Assert.AreEqual(1, titleCount);
        }

        [TestMethod]
        public void GetCountWithParentTag()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "memes" } } };

            // Filter by a tag that is a parent of another tag
            int parentTagCount = MappingRepository.GetMappingsCount(collectBy, filterBy);

            Assert.AreEqual(1, parentTagCount);
        }

        [TestMethod]
        public void GetCountWithAncestorTag()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new string[0] };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "internet stuff" } } };

            // Filter by a tag that is a parent of another tag
            int ancestorTagCount = MappingRepository.GetMappingsCount(collectBy, filterBy);

            Assert.AreEqual(1, ancestorTagCount);
        }

        [TestMethod]
        public void GetCountCollectedBySingleNamespace()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new[] { "title" } };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:white image" } } };

            // Filter by a tag that is a parent of another tag
            int collectedByTitleCount = MappingRepository.GetMappingsCount(collectBy, filterBy);

            Assert.AreEqual(1, collectedByTitleCount);
        }

        [TestMethod]
        public void GetCountCollectedByMultipleNamespaces()
        {
            MediaCollectViewModel collectBy = new MediaCollectViewModel { Namespaces = new[] { "title", "series" } };
            SearchPredicateViewModel[] filterBy = new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:white image" } } };

            // Filter by a tag that is a parent of another tag
            int collectedByTitleAndSeriesCount = MappingRepository.GetMappingsCount(collectBy, filterBy);

            Assert.AreEqual(2, collectedByTitleAndSeriesCount);
        }
    }
}
