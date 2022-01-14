using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SqlStitcher.Forms.Contracts;
using SqlStitcher.Forms.Infrastructure;

namespace SqlStitcher.Test.Forms.Infrastructure
{
    [TestClass]
    public class RecentFileListTests
    {
        public Mock<ISettings> _mockSettings = new Mock<ISettings>();
        public Mock<IMessenger> _mockMessenger = new Mock<IMessenger>();

        [TestInitialize]
        public void InitializeTest()
        {
            _mockSettings.SetupGet(p => p.RecentListLength).Returns(5);
            _mockSettings.SetupGet(p => p.RecentList).Returns("");
        }

        [TestMethod]
        public void PushOne()
        {
            // Setup
            var recentFileList = new RecentFileList(_mockSettings.Object, _mockMessenger.Object);

            // Execute
            recentFileList.Push("one");

            // Assert
            Assert.AreEqual(1, recentFileList.Count());
            Assert.AreEqual("one", recentFileList.First());
        }


        [TestMethod]
        public void PushOneTwice()
        {
            // Setup
            var recentFileList = new RecentFileList(_mockSettings.Object, _mockMessenger.Object);

            // Execute
            recentFileList.Push("one");
            recentFileList.Push("one");

            // Assert
            Assert.AreEqual(1, recentFileList.Count());
            Assert.AreEqual("one", recentFileList.First());
        }


        [TestMethod]
        public void PushThree()
        {
            // Setup
            var recentFileList = new RecentFileList(_mockSettings.Object, _mockMessenger.Object);

            // Execute
            recentFileList.Push("one");
            recentFileList.Push("two");
            recentFileList.Push("three");

            // Assert
            Assert.AreEqual(3, recentFileList.Count());
            Assert.AreEqual("three", recentFileList.Skip(0).First());
            Assert.AreEqual("two", recentFileList.Skip(1).First());
            Assert.AreEqual("one", recentFileList.Skip(2).First());
        }

        [TestMethod]
        public void PushSix()
        {
            // Setup
            var recentFileList = new RecentFileList(_mockSettings.Object, _mockMessenger.Object);

            // Execute
            recentFileList.Push("one");
            recentFileList.Push("two");
            recentFileList.Push("three");
            recentFileList.Push("four");
            recentFileList.Push("five");
            recentFileList.Push("six");

            // Assert
            Assert.AreEqual(5, recentFileList.Count());
            Assert.AreEqual("six", recentFileList.Skip(0).First());
            Assert.AreEqual("five", recentFileList.Skip(1).First());
            Assert.AreEqual("four", recentFileList.Skip(2).First());
            Assert.AreEqual("three", recentFileList.Skip(3).First());
            Assert.AreEqual("two", recentFileList.Skip(4).First());
        }
        
    }
}
