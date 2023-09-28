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
        public void GetMatchingMappings()
        {
            IEnumerable<CurrentMapping> noFilterMappings = MappingRepository.GetMatchingMappings(
                new MediaCollectViewModel { Namespaces = new[] { "title" } },
                new MediaSortViewModel { Namespaces = new[] { "title" } },
                null,
                0,
                100
            );

            Assert.IsNotNull(noFilterMappings);
            Assert.AreEqual(0, noFilterMappings.Count());

            IEnumerable<CurrentMapping> notFoundMappings = MappingRepository.GetMatchingMappings(
                new MediaCollectViewModel { Namespaces = new[] { "title" } },
                new MediaSortViewModel { Namespaces = new[] { "title" } },
                new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "werjfuwfwuiefhiu" } } },
                0,
                100
            );

            Assert.IsNotNull(notFoundMappings);
            Assert.AreEqual(0, notFoundMappings.Count());

            IEnumerable<CurrentMapping> titleMappings = MappingRepository.GetMatchingMappings(
                new MediaCollectViewModel { Namespaces = new[] { "title" } },
                new MediaSortViewModel { Namespaces = new[] { "title" } },
                new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:have a relaxing kappachino" } } },
                0,
                100
            );

            Assert.IsNotNull(titleMappings);
            Assert.AreEqual(1, titleMappings.Count());

            IEnumerable<CurrentMapping> parentTagMappings = MappingRepository.GetMatchingMappings(
                new MediaCollectViewModel { Namespaces = new[] { "title" } },
                new MediaSortViewModel { Namespaces = new[] { "title" } },
                new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "memes" } } },
                0,
                100
            );

            Assert.IsNotNull(parentTagMappings);
            Assert.AreEqual(1, parentTagMappings.Count());

            IEnumerable<CurrentMapping> ancestorTagMappings = MappingRepository.GetMatchingMappings(
                new MediaCollectViewModel { Namespaces = new[] { "title" } },
                new MediaSortViewModel { Namespaces = new[] { "title" } },
                new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "internet stuff" } } },
                0,
                100
            );

            Assert.IsNotNull(ancestorTagMappings);
            Assert.AreEqual(1, ancestorTagMappings.Count());
        }

        [TestMethod]
        public void GetCount()
        {
            int noFilterCount = MappingRepository.GetMappingsCount(
               null
            );

            Assert.AreEqual(0, noFilterCount);

            int notFoundCount = MappingRepository.GetMappingsCount(
               new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "werjfuwfwuiefhiu" } } }
            );

            Assert.AreEqual(0, notFoundCount);

            int titleCount = MappingRepository.GetMappingsCount(
               new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:have a relaxing kappachino" } } }
            );

            Assert.AreEqual(1, titleCount);

            int parentTagCount = MappingRepository.GetMappingsCount(
               new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "memes" } } }
            );

            Assert.AreEqual(1, parentTagCount);

            int ancestorTagCount = MappingRepository.GetMappingsCount(
               new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "internet stuff" } } }
            );

            Assert.AreEqual(1, ancestorTagCount);
        }
    }
}
