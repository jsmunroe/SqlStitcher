using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlStitcher.Models;

namespace SqlStitcher.Test.App.Models
{
    [TestClass]
    public class ScriptBatchTests
    {

        [TestMethod]
        public void Construct()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            var scripts = new[]
            {
                TestState.GetEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql"),
                TestState.GetEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql"),
                TestState.GetEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql")
            };

            // Execute
            var scriptBatch = new ScriptBatch(project, scripts);

            // Assert
            Assert.AreEqual(3, scriptBatch.Count());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullProject()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            var scripts = new[]
            {
                TestState.GetEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql"),
                TestState.GetEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql"),
                TestState.GetEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql")
            };

            // Execute
            new ScriptBatch(project: null, entries: scripts);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullScriptEntries()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();

            // Execute
            new ScriptBatch(project, entries: null);
        }


        [TestMethod]
        public void ConstructWithEmptyScriptEntries()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            var scripts = new ScriptEntry[] { };

            // Execute
            var scriptBatch = new ScriptBatch(project, scripts);

            // Assert
            Assert.AreEqual(0, scriptBatch.Count());
        }


        [TestMethod]
        public void GetScripts()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            var scripts = new[]
            {
                TestState.GetEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql"),
                TestState.GetEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql"),
                TestState.GetEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql")
            };
            var scriptBatch = new ScriptBatch(project, scripts);

            var buildOptions = new BuildOptions
            {
                InsertGoCommand = false,
                PrependScriptDescription = PrependDescription.None,
            };


            // Execute
            var result = scriptBatch.GetScript(buildOptions);

            // Assert
            var length = scripts.Sum(p => p.ReadToEnd().Length + 4);
            Assert.AreEqual(length, result.Length);
        }
      
    }
}
