using System;
using Helpers.IO;
using Helpers.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlStitcher.Models;

namespace SqlStitcher.Test.App.Models
{
    [TestClass]
    public class ScriptEntryTests
    {
        [TestMethod]
        public void Build()
        {
            // Setup
            var fileSystem = new TestFileSystem();
            var file = fileSystem.StageFile(@"C:\script.sql");

            // Execute
            var result = ScriptEntry.Build(file);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("script.sql", result.Name);
            Assert.AreSame(file, result.File);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuildWithNullFile()
        {
            // Execute
            ScriptEntry.Build(file: null);
        }


        [TestMethod]
        public void ReadToEnd()
        {
            // Setup
            var file = new FsFile(TestState.Root.Child(@"Scripts\Master\Views\ClaimView.sql"));
            var scriptEntry = ScriptEntry.Build(file);

            // Execute
            var scriptText = scriptEntry.ReadToEnd();

            // Assert
            Assert.IsNotNull(scriptText);
            Assert.AreEqual(28403, scriptText.Length);
        }


        [TestMethod]
        public void EqualsOnDifferentInstancesThatAreEqual()
        {
            // Setup
            var file1 = new FsFile(TestState.Root.Child(@"Scripts\Master\Views\ClaimView.sql"));
            var scriptEntry1 = ScriptEntry.Build(file1);
            var file2 = new FsFile(TestState.Root.Child(@"Scripts\Master\Views\ClaimView.sql"));
            var scriptEntry2 = ScriptEntry.Build(file2);

            // Execute
            var result = scriptEntry1.Equals(scriptEntry2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualsOnDifferentInstancesThatAreNotEqual()
        {
            // Setup
            var file1 = new FsFile(TestState.Root.Child(@"Scripts\Master\Views\ClaimView.sql"));
            var scriptEntry1 = ScriptEntry.Build(file1);
            var file2 = new FsFile(TestState.Root.Child(@"Scripts\Master\Views\ClaimWitnessView.sql"));
            var scriptEntry2 = ScriptEntry.Build(file2);

            // Execute
            var result = scriptEntry1.Equals(scriptEntry2);

            // Assert
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void EqualsWithNull()
        {
            // Setup
            var file = new FsFile(TestState.Root.Child(@"Scripts\Master\Views\ClaimView.sql"));
            var scriptEntry = ScriptEntry.Build(file);

            // Execute
            var result = scriptEntry.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void EqualsWhenCaseDiffers()
        {
            // Setup
            var file1 = new FsFile(TestState.Root.Child(@"Scripts\Master\Views\ClaimView.sql"));
            var scriptEntry1 = ScriptEntry.Build(file1);
            var file2 = new FsFile(TestState.Root.Child(@"scripts\master\views\claimview.sql"));
            var scriptEntry2 = ScriptEntry.Build(file2);

            // Execute
            var result = scriptEntry1.Equals(scriptEntry2);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(file1.Path.Equals(file2.Path));
        }


        [TestMethod]
        public void ToXmlAndFromXml()
        {
            // Setup
            var file = new FsFile(TestState.Root.Child(@"Scripts\Master\Views\ClaimView.sql"));
            var scriptEntry = ScriptEntry.Build(file);
            scriptEntry.IsSelected = false;

            // Execute
            var xml = scriptEntry.ToXml();
            var loadedScriptEntry = ScriptEntry.FromXml(xml);

            // Assert
            Assert.AreEqual(scriptEntry.File.Path, loadedScriptEntry.File.Path);
            Assert.AreEqual(scriptEntry.Name, loadedScriptEntry.Name);
            Assert.AreEqual(scriptEntry.IsSelected, loadedScriptEntry.IsSelected);
        }

        
    }
}
