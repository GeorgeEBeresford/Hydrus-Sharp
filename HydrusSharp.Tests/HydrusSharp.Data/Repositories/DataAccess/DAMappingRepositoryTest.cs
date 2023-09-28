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
            IEnumerable<CurrentMapping> matchingMappings = MappingRepository.GetMatchingMappings(
                new MediaCollectViewModel { Namespaces = new[] { "title" } },
                new MediaSortViewModel { Namespaces = new[] { "title" } },
                new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:have a relaxing kappachino" } } },
                0,
                100
            );

            Assert.IsNotNull(matchingMappings);
            Assert.AreEqual(1, matchingMappings.Count());
        }

        [TestMethod]
        public void GetCount()
        {
            int count = MappingRepository.GetMappingsCount(
               new[] { new SearchPredicateViewModel { MustBeTrue = true, SearchType = PredicateType.PREDICATE_TYPE_TAG, SearchData = new[] { "title:have a relaxing kappachino" } } }
            );

            Assert.AreEqual(1, count);
        }
    }
}
