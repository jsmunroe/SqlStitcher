using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlStitcher.Helpers;
using SqlStitcher.Models;

namespace SqlStitcher.Test.App.Helpers
{
    [TestClass]
    public class XmlHelpersTests
    {

        [TestMethod]
        public void FromXml()
        {
            // Setup
            var xml = XElement.Parse(@"<ScriptEntry Version=""1"" IsSelected=""false"">C:\Users\munroej\Source\SqlStitcher\SqlStitcher.Test\bin\Debug\Files\Scripts\Master\Views\ClaimView.sql</ScriptEntry>");

            // Execute
            var result = XmlHelpers.FromXml<ScriptEntry>(xml);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(@"C:\Users\munroej\Source\SqlStitcher\SqlStitcher.Test\bin\Debug\Files\Scripts\Master\Views\ClaimView.sql", result.File.Path);
            Assert.IsFalse(result.IsSelected);
        }

      
    }
}
