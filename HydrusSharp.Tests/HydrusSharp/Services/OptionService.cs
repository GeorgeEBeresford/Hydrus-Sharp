using HydrusSharp.Data.Models.Client;
using HydrusSharp.Data.Repositories;
using HydrusSharp.Data.Repositories.DataAccess;
using HydrusSharp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HydrusSharp.Tests.HydrusSharp.Services
{
    [TestClass]
    public class OptionServiceTest
    {
        private OptionService OptionService { get; set; }

        public OptionServiceTest()
        {
            OptionService = new OptionService();
        }

        [TestMethod]
        public void GetSingleValueOption()
        {
            IOptionRepository optionRepository = new DAOptionRepository();
            OptionCollection optionCollection = optionRepository.GetOptions();

            string deleteToRecycleBin = OptionService.GetValue(optionCollection, "delete_to_recycle_bin");
            Assert.IsNotNull(deleteToRecycleBin, "deleteToRecycleBin is null");
            Assert.IsTrue(deleteToRecycleBin == "false" || deleteToRecycleBin == "true", $"deleteToRecycleBin does not match expected output. Actual output: '{deleteToRecycleBin}'");
        }

        [TestMethod]
        public void GetMultipleValueOption()
        {
            IOptionRepository optionRepository = new DAOptionRepository();
            OptionCollection optionCollection = optionRepository.GetOptions();

            string dimensions = OptionService.GetValue(optionCollection, "thumbnail_dimensions");
            Assert.IsNotNull(dimensions, "dimensions is null");
            Assert.IsTrue(dimensions == "1280\n720" || dimensions == "true", $"dimensions does not match expected output. Actual output: '{dimensions}'");
        }
    }
}
