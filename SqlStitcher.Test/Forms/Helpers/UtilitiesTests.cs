using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlStitcher.Forms.Helpers;

namespace SqlStitcher.Test.Forms.Helpers
{
    [TestClass]
    public class UtilitiesTests
    {
        [TestMethod]
        public void ShortenPath()
        {
            // Setup
            var path = @"C:\TFS\IOA\IOA.RiskServices\DevCurrent\DevMain";

            // Execute
            var result = Utilities.ShortenPath(path);

            // Setup
            Assert.AreEqual(@"C:\…\DevCurrent\DevMain", result);
        }


        [TestMethod]
        public void ShortenPathWithReallyLongFileName()
        {
            // Setup
            var path = @"C:\TFS.IOA.IOA.RiskServices.DevCurrent.DevMain";

            // Execute
            var result = Utilities.ShortenPath(path);

            // Setup
            Assert.AreEqual(@"C:\…es.DevCurrent.DevMain", result);
        }


        [TestMethod]
        public void ShortenPathWithNetworkShare()
        {
            // Setup
            var path = @"\\Share\TFS\IOA\IOA.RiskServices\DevCurrent\DevMain";

            // Execute
            var result = Utilities.ShortenPath(path);

            // Setup
            Assert.AreEqual(@"\\Share\…\DevMain", result);
        }


        [TestMethod]
        public void ShortenPathWithNoRoot()
        {
            // Setup
            var path = @"TFS\IOA\IOA.RiskServices\DevCurrent\DevMain";

            // Execute
            var result = Utilities.ShortenPath(path);

            // Setup
            Assert.AreEqual(@"…\DevCurrent\DevMain", result);
        }


        [TestMethod]
        public void ShortenEmptyPath()
        {
            // Setup
            var path = @"";

            // Execute
            var result = Utilities.ShortenPath(path);

            // Setup
            Assert.AreEqual(@"", result);
        }      


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShortenPathWithNullPath()
        {
            // Execute
            Utilities.ShortenPath(path: null);
        }

      
    }
}
