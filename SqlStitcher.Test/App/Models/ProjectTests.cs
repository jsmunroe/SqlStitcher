using System;
using System.Linq;
using Helpers.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlStitcher.Models;

namespace SqlStitcher.Test.App.Models
{
    [TestClass]
    public class ProjectTests
    {
        [TestMethod]
        public void Construct()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts\Master\Views");
            var projectRoot = new FsDirectory(projectRootPath);
            
            // Execute
            var project = new Project(projectRoot);

            // Assert
            Assert.AreSame(projectRoot, project.Root);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNull()
        {
            // Execute
            new Project(root: null);
        }

        [TestMethod]
        public void Load()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);

            // Execute
            project.Load();

            // Assert
            Assert.IsNotNull(project.Entries);
            Assert.AreEqual(320, project.Entries.Count);
        }


        [TestMethod]
        public void Build()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            project.SelectAll();

            // Execute
            var result = project.Build();

            // Assert
            Assert.AreNotEqual(String.Empty, result);
        }


        [TestMethod]
        public void BuildWithNoEntriesSelected()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();

            // Execute
            var result = project.Build();

            // Assert
            Assert.AreEqual(0, result.Length);
        }


        [TestMethod]
        public void Select()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            var entries = project.Entries.Take(3);

            // Execute
            project.Select(entries);

            // Assert
            Assert.IsTrue(project.Entries.Take(3).All(p => p.IsSelected));
            Assert.IsTrue(project.Entries.Skip(3).All(p => !p.IsSelected));
        }


        [TestMethod]
        public void SelectWithSeparateInstances()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            var entries = project.Entries.Take(3).Select(p => ScriptEntry.Build(p.File));

            // Execute
            project.Select(entries);

            // Assert
            Assert.IsTrue(project.Entries.Take(3).All(p => p.IsSelected));
            Assert.IsTrue(project.Entries.Skip(3).All(p => !p.IsSelected));
        }

        [TestMethod]
        public void SelectAll()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();

            // Execute
            project.SelectAll();

            // Assert
            Assert.IsFalse(project.Entries.Any(p => !p.IsSelected));
        }


        [TestMethod]
        public void UnselectAll()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            project.SelectAll();

            // Execute
            project.UnselectAll();

            // Assert
            Assert.IsFalse(project.Entries.Any(p => p.IsSelected));
        }


        [TestMethod]
        public void GetIdentifiers()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql");

            // Execute
            var identifiers = project.GetIdentifiers();

            // Assert
            Assert.AreEqual(14, identifiers.Count());
        }


        [TestMethod]
        public void ReplaceIdentifier()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql");

            // Execute
            var result = project.ReplaceIdentifier("IOA.RiskServices", "IOA.RiskServices.Training");

            // Assert
            var scriptText = project.Build();

            Assert.IsTrue(result);
            Assert.IsTrue(scriptText.Contains("[IOA.RiskServices.Training]"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceIdentifierWithNullText()
        {
            // Setup
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql");


            // Execute
            var result = project.ReplaceIdentifier(text: null, replacementText: "[IOA.RiskServices.Training]");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceIdentifierWithNullReplacementText()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql");

            // Execute
            var result = project.ReplaceIdentifier("[IOA.RiskServices]", replacementText: null);
        }


        [TestMethod]
        public void ReplaceNotExistingIdentifier()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql");

            // Execute
            var result = project.ReplaceIdentifier("IOA.RiskServices.Training", "IOA.SomethingElse");

            // Assert
            var scriptText = project.Build();

            Assert.IsFalse(result);
            Assert.IsFalse(scriptText.Contains("[IOA.SomethingElse]"));
        }


        [TestMethod]
        public void BuildTree()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            project.SelectAll();

            // Execute
            var result = project.BuildTree();

            // Assert
            Assert.AreEqual(projectRootPath, result.Description);
        }


        [TestMethod]
        public void RevertIdentifiers()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql");
            TestState.SelectEntry(project, @"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql");
            project.ReplaceIdentifier("IOA.RiskServices", "IOA.RiskServices.Training");

            // Execute
            project.Revertidentifiers();

            // Assert
            var scriptText = project.Build();

            Assert.IsTrue(scriptText.Contains("[IOA.RiskServices]"));
        }


        [TestMethod]
        public void BuildWithSecificScripts()
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

            var buildOptions = new BuildOptions
            {
                InsertGoCommand = false,
                PrependScriptDescription = PrependDescription.None,
            };

            // Execute
            var result = project.Build(scripts, buildOptions);

            // Assert
            var length = scripts.Sum(p => p.ReadToEnd().Length + 4);
            Assert.AreEqual(length, result.Length);
        }


        [TestMethod]
        public void ToXmlAndFromXml()
        {
            // Setup
            var projectRootPath = TestState.Root.Child(@"\Scripts");
            var projectRoot = new FsDirectory(projectRootPath);
            var project = new Project(projectRoot);
            project.Load();
            project.Select(project.Entries.Take(3));

            // Execute
            var xml = project.ToXml();
            var loadedProject = Project.FromXml(xml);

            // Assert
            Assert.AreEqual(project.Root.Path, loadedProject.Root.Path);
            Assert.IsTrue(loadedProject.Entries.Take(3).All(p => p.IsSelected));
            Assert.IsTrue(loadedProject.Entries.Skip(3).All(p => !p.IsSelected));
        }
    }
}
