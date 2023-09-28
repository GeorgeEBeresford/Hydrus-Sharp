using HydrusSharp.Data.Models.Client;
using HydrusSharp.Data.Repositories.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace HydrusSharp.Tests.HydrusSharp.Data.Repositories.DataAccess
{
    [TestClass]
    public class DAFileRepositoryTest
    {
        private DAFileRepository FileRepository { get; set; }

        public DAFileRepositoryTest()
        {
            FileRepository = new DAFileRepository();
        }

        [TestMethod]
        public void GetFileInfos()
        {
            IEnumerable<FileInfo> fileInfos = FileRepository.GetFileInfos(new[] { 1 });

            Assert.IsNotNull( fileInfos );
            Assert.AreEqual(1, fileInfos.Count());
        }
    }
}
