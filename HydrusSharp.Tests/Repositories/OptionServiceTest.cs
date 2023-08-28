using HydrusSharp.DbContexts;
using HydrusSharp.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HydrusSharp.Tests.Repositories
{
    [TestClass]
    public class OptionRepositoryTest
    {
        private ClientDbContext ClientDbContext { get; set; }
        private OptionRepository OptionService { get; set; }

        public OptionRepositoryTest()
        {
            ClientDbContext = new ClientDbContext();
            OptionService = new OptionRepository(ClientDbContext);
        }

        [TestMethod]
        public void GetOption()
        {
            string option = OptionService.GetOption("delete_to_recycle_bin");
            Assert.IsNotNull(option, "Option is null");
            Assert.IsTrue(option == "false" || option == "true", $"Option does not match expected output. Actual output: '{option}'");
        }
    }
}
