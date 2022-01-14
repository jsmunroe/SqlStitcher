using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Helpers.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlStitcher.Models;

namespace SqlStitcher.Test.App
{
    [TestClass]
    public class ScriptBuilderTests
    {
        [TestMethod]
        public void Construct()
        {
            // Execute
            new ScriptBuilder();
        }

        [TestMethod]
        public void Append()
        {
            // Setup
            var scriptEntry = ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Master\Views\ClaimView.sql")));
            var scriptBuilder = new ScriptBuilder();

            // Execute
            scriptBuilder.Append(scriptEntry);

            // Assert
            Assert.AreEqual(1, scriptBuilder.Entries.Count);
            CollectionAssert.AreEqual(new[] {scriptEntry}, scriptBuilder.Entries.ToArray());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AppendWithNullScriptEntry()
        {
            // Setup
            var scriptBuilder = new ScriptBuilder();

            // Execute
            scriptBuilder.Append(scriptEntry: null);
        }

        [TestMethod]
        public void AppendRange()
        {
            // Setup
            var scriptEntries = new[]
            {
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.5.0.0\10_SchemaAndData\1000_Alter_ClaimWitnessView.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.5.0.0\10_SchemaAndData\1100_Alter_ClaimView.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.5.0.0\10_SchemaAndData\1200_Alter_GeneralLiabilityClaimView.sql")))
            };
            var scriptBuilder = new ScriptBuilder();

            // Execute
            scriptBuilder.AppendRange(scriptEntries);

            // Assert
            Assert.AreEqual(3, scriptBuilder.Entries.Count);
            CollectionAssert.AreEqual(scriptEntries, scriptBuilder.Entries.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AppendRangeWithNull()
        {
            // Setup
            var scriptBuilder = new ScriptBuilder();

            // Execute
            scriptBuilder.AppendRange(scriptEntries: null);
        }

        [TestMethod]
        public void Build()
        {
            // Setup
            var scriptEntries = new[]
            {
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql")))
            };
            var scriptBuilder = new ScriptBuilder();
            scriptBuilder.AppendRange(scriptEntries);

            // Execute
            var scriptText = scriptBuilder.Build();

            // Assert
            Assert.IsTrue(Regex.IsMatch(scriptText, @"\[IOA\.RiskServices]"));
        }

        [TestMethod]
        public void BuildWhenEmpty()
        {
            // Setup
            var scriptBuilder = new ScriptBuilder();

            // Execute
            var scriptText = scriptBuilder.Build();

            // Assert
            Assert.AreEqual(0, scriptText.Length);
        }

        [TestMethod]
        public void BuildWithGO()
        {
            // Setup
            var scriptEntries = new[]
            {
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql")))
            };
            var scriptBuilder = new ScriptBuilder();
            scriptBuilder.AppendRange(scriptEntries);

            var buildOptions = new BuildOptions
            {
                InsertGoCommand = true,
            };

            // Execute
            var scriptText = scriptBuilder.Build(buildOptions);

            // Assert
            Assert.IsTrue(Regex.IsMatch(scriptText, @"\r\n\r\nGO\r\n\r\n"));

        }

        [TestMethod]
        public void BuildWithoutGO()
        {
            // Setup
            var scriptEntries = new[]
            {
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql")))
            };
            var scriptBuilder = new ScriptBuilder();
            scriptBuilder.AppendRange(scriptEntries);

            var buildOptions = new BuildOptions
            {
                InsertGoCommand = false,
            };

            // Execute
            var scriptText = scriptBuilder.Build(buildOptions);

            // Assert
            Assert.IsFalse(Regex.IsMatch(scriptText, @"\r\n\r\nGO\r\n\r\n"));
        }

        [TestMethod]
        public void BuildWithPrependDescriptionFileName()
        {
            // Setup
            var scriptEntries = new[]
            {
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql")))
            };
            var scriptBuilder = new ScriptBuilder();
            scriptBuilder.AppendRange(scriptEntries);

            var buildOptions = new BuildOptions
            {
                PrependScriptDescription = PrependDescription.FileName,
            };

            // Execute
            var scriptText = scriptBuilder.Build(buildOptions);

            // Assert
            Assert.IsTrue(Regex.IsMatch(scriptText, @"2000_Rename_ClientLocationGroup_to_ClientGroup.sql"));
        }


        [TestMethod]
        public void BuildWithPrependDescriptionFileFullPath()
        {
            // Setup
            var scriptEntries = new[]
            {
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql")))
            };
            var scriptBuilder = new ScriptBuilder();
            scriptBuilder.AppendRange(scriptEntries);

            var buildOptions = new BuildOptions
            {
                PrependScriptDescription = PrependDescription.FileFullPath,
            };

            // Execute
            var scriptText = scriptBuilder.Build(buildOptions);

            // Assert
            Assert.IsTrue(Regex.IsMatch(scriptText, @"Scripts\\Release2\\Release2\.4\.0\.0\\10_SchemaAndData\\2000_Rename_ClientLocationGroup_to_ClientGroup.sql"));
        }


        [TestMethod]
        public void BuildWithPrependDescriptionNone()
        {
            // Setup
            var scriptEntries = new[]
            {
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2000_Rename_ClientLocationGroup_to_ClientGroup.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2010_Create_ClientGroupType_Table.sql"))),
                ScriptEntry.Build(new FsFile(TestState.Root.Child(@"Scripts\Release2\Release2.4.0.0\10_SchemaAndData\2020_Add_Permission_Columns_to_ClientGroup.sql")))
            };
            var scriptBuilder = new ScriptBuilder();
            scriptBuilder.AppendRange(scriptEntries);

            var buildOptions = new BuildOptions
            {
                PrependScriptDescription = PrependDescription.None,
            };

            // Execute
            var scriptText = scriptBuilder.Build(buildOptions);

            // Assert
            Assert.IsFalse(Regex.IsMatch(scriptText, @"Scripts\\Release2\\Release2\.4\.0\.0\\10_SchemaAndData\\2000_Rename_ClientLocationGroup_to_ClientGroup.sql"));
        }

    }
}
