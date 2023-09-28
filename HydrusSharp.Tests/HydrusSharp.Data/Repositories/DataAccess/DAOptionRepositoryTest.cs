using HydrusSharp.Data.Models.Client;
using HydrusSharp.Data.Repositories.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HydrusSharp.Tests.HydrusSharp.Data.Repositories.DataAccess
{
    [TestClass]
    public class DAOptionRepositoryTest
    {
        private DAOptionRepository OptionRepository { get; set; }

        public DAOptionRepositoryTest()
        {
            OptionRepository = new DAOptionRepository();
        }

        [TestMethod]
        public void GetOption()
        {
            OptionCollection optionCollection = OptionRepository.GetOptions();

            Assert.IsNotNull(optionCollection);
        }
    }
}
