using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlStitcher.Test.Forms;

namespace SqlStitcher.Test
{
    [TestClass]
    public class SetupAssemblyInitializer
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            TestState.MockContainerFactory = new MockContainerFactory();
            TestState.MockContainerFactory.Create();
        }
    }
}
