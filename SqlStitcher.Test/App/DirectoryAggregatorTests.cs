using System;
using System.Linq;
using Helpers.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SqlStitcher.Test.App
{
    [TestClass]
    public class DirectoryAggregatorTests
    {
        [TestMethod]
        public void Construct()
        {
            // Setup
            var fileSystem = new TestFileSystem();
            var directory = fileSystem.StageDirectory(@"C:\TestDirectory");

            // Execute
            var aggregator = new DirectoryAggregator(directory);

            // Assert
            Assert.AreSame(aggregator.Root, directory); 
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNull()
        {
            // Execute
            new DirectoryAggregator(root: null);
        }


        [TestMethod]
        public void GetEntries()
        {
            // Setup
            var fileSystem = new TestFileSystem();
            var directory = fileSystem.StageDirectory(@"C:\TestDirectory");
            fileSystem.StageFile(@"C:\TestDirectory\One\entry1.sql");
            fileSystem.StageFile(@"C:\TestDirectory\One\entry2.sql");
            fileSystem.StageFile(@"C:\TestDirectory\One\entry3.sql");
            fileSystem.StageFile(@"C:\TestDirectory\Two\Three\Four\entry4.sql");

            var aggregator = new DirectoryAggregator(directory);

            // Execute
            var result = aggregator.GetEntries();
            
            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
            CollectionAssert.AreEquivalent(new[] { "entry1.sql", "entry2.sql", "entry3.sql", "entry4.sql" }, result.Select(p => p.Name).ToList());
        }


        [TestMethod]
        public void BuildTree()
        {
            // Setup
            var fileSystem = new TestFileSystem();
            var directory = fileSystem.StageDirectory(@"C:\TestDirectory");
            fileSystem.StageFile(@"C:\TestDirectory\One\entry1.sql");
            fileSystem.StageFile(@"C:\TestDirectory\One\entry2.sql");
            fileSystem.StageFile(@"C:\TestDirectory\One\entry3.sql");
            fileSystem.StageFile(@"C:\TestDirectory\Two\Three\Four\entry4.sql");

            var aggregator = new DirectoryAggregator(directory);

            // Execute
            var result = aggregator.BuildTree();

            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(@"C:\TestDirectory", result.Description);
            Assert.AreEqual(2, result.Children.Count);
        }

        [TestMethod]
        public void GetEntriesTwice()
        {
            // Setup
            var fileSystem = new TestFileSystem();
            var directory = fileSystem.StageDirectory(@"C:\TestDirectory");
            fileSystem.StageFile(@"C:\TestDirectory\One\entry1.sql");

            var aggregator = new DirectoryAggregator(directory);

            // Execute
            var result1 = aggregator.GetEntries().First();
            var result2 = aggregator.GetEntries().First();

            // Assert 
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreSame(result1, result2);
        }


        [TestMethod]
        public void BuildTreeTwice()
        {
            // Setup
            var fileSystem = new TestFileSystem();
            var directory = fileSystem.StageDirectory(@"C:\TestDirectory");
            fileSystem.StageFile(@"C:\TestDirectory\One\entry1.sql");

            var aggregator = new DirectoryAggregator(directory);

            // Execute
            var result1 = aggregator.BuildTree().Children.First().Children.First().Value;
            var result2 = aggregator.BuildTree().Children.First().Children.First().Value;

            // Assert 
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual(result1, result2);
            Assert.AreSame(result1, result2);
        }

        [TestMethod]
        public void GetEntriesThenBuldTree()
        {
            // Setup
            var fileSystem = new TestFileSystem();
            var directory = fileSystem.StageDirectory(@"C:\TestDirectory");
            fileSystem.StageFile(@"C:\TestDirectory\One\entry1.sql");

            var aggregator = new DirectoryAggregator(directory);

            // Execute
            var result1 = aggregator.GetEntries().First();
            var result2 = aggregator.BuildTree().Children.First().Children.First().Value;

            // Assert 
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual(result1, result2);
            Assert.AreSame(result1, result2);
        }


        [TestMethod]
        public void GetEntriesThenAgainAfterAddingFile()
        {
            // Setup
            var fileSystem = new TestFileSystem();
            var directory = fileSystem.StageDirectory(@"C:\TestDirectory");
            fileSystem.StageFile(@"C:\TestDirectory\One\entry1.sql");

            var aggregator = new DirectoryAggregator(directory);

            // Execute
            var result1 = aggregator.GetEntries();
            fileSystem.StageFile(@"C:\TestDirectory\One\entry2.sql");
            var result2 = aggregator.GetEntries();

            // Assert 
            Assert.AreEqual(1, result1.Count());
            Assert.AreEqual(2, result2.Count());
            Assert.IsTrue(result2.Any(p => ReferenceEquals(p, result1.First())));
        }

        [TestMethod]
        public void GetEntriesThenAgainAfterRemovingFile()
        {
            // Setup
            var fileSystem = new TestFileSystem();
            var directory = fileSystem.StageDirectory(@"C:\TestDirectory");
            fileSystem.StageFile(@"C:\TestDirectory\One\entry1.sql");

            var aggregator = new DirectoryAggregator(directory);

            // Execute
            var result1 = aggregator.GetEntries();
            fileSystem.DeleteFile(@"C:\TestDirectory\One\entry1.sql");
            var result2 = aggregator.GetEntries();

            // Assert 
            Assert.AreEqual(1, result1.Count());
            Assert.AreEqual(0, result2.Count());
        }

    }
}
