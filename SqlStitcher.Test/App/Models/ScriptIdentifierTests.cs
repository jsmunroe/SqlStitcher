using System;
using Helpers.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlStitcher.Models;

namespace SqlStitcher.Test.App.Models
{
    [TestClass]
    public class ScriptIdentifierTests
    {
        [TestMethod]
        public void Construct()
        {
            // Execute
            new ScriptIdentifier();
        }

        [TestMethod]
        public void Replace()
        {
            // Setup
            var scriptIdentifier = new ScriptIdentifier
            {
                Name = "IOA.RiskServices",
                ReplacementName = "IOA.SomethingElse"
            };
            var scriptText = "USE [IOA.RiskServices]\nINSERT INTO [schema].[Table](Column1, Column2)\nUPDATE [schema].[Table]";

            // Execute
            var result = scriptIdentifier.Replace(scriptText);

            // Assert
            Assert.AreEqual("USE [IOA.SomethingElse]\nINSERT INTO [schema].[Table](Column1, Column2)\nUPDATE [schema].[Table]", result);
        }
        
        [TestMethod]
        public void ReplaceWithCaseMismatch()
        {
            // Setup
            var scriptIdentifier = new ScriptIdentifier
            {
                Name = "IOA.RiskServices",
                ReplacementName = "IOA.SomethingElse"
            };
            var scriptText = "USE [ioa.riskservices]\nINSERT INTO [schema].[Table](Column1, Column2)\nUPDATE [schema].[Table]";

            // Execute
            var result = scriptIdentifier.Replace(scriptText);

            // Assert
            Assert.AreEqual("USE [IOA.SomethingElse]\nINSERT INTO [schema].[Table](Column1, Column2)\nUPDATE [schema].[Table]", result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceWithNull()
        {
            // Setup
            var scriptIdentifier = new ScriptIdentifier
            {
                Name = "[IOA.RiskServices]",
                ReplacementName = "[IOA.SomethingElse]"
            };

            // Execute
            scriptIdentifier.Replace(scriptText: null);
        }


        [TestMethod]
        public void ToXmlAndFromXml()
        {
            // Setup
            var scriptIdentifier = new ScriptIdentifier
            {
                Name = "IOA.RiskServices",
                ReplacementName = "IOA.SomethingElse"
            };

            // Execute
            var xml = scriptIdentifier.ToXml();
            var loadedScriptIdentifier = ScriptIdentifier.FromXml(xml);

            // Assert
            Assert.AreEqual(scriptIdentifier.Name, loadedScriptIdentifier.Name);
            Assert.AreEqual(scriptIdentifier.ReplacementName, loadedScriptIdentifier.ReplacementName);
        }

    }
}
