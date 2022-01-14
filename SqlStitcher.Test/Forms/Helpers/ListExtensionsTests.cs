using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SqlStitcher.Test.Forms.Helpers
{
    [TestClass]
    public class ListExtensionsTests
    {
        [TestMethod]
        public void MoveToUpward()
        {
            // Setup
            var list = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}.ToList();

            // Execute
            var result = list.MoveTo(1, 2);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(new[] { 2, 3, 1, 4, 5, 6, 7, 8, 9, 10 }.ToList(), list);
        }


        [TestMethod]
        public void MoveToDownward()
        {
            // Setup
            var list = new[] { "Apple", "Banana", "Carrot", "Egg Plant", "Fish Tacos" }.ToList();

            // Execute
            var result = list.MoveTo("Egg Plant", 2);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(new[] { "Apple", "Banana", "Egg Plant", "Carrot", "Fish Tacos" }.ToList(), list);
        }


        [TestMethod]
        public void MoveToBeginning()
        {
            // Setup
            var list = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}.ToList();

            // Execute
            var result = list.MoveTo(4, 0);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(new[] { 4, 1, 2, 3, 5, 6, 7, 8, 9, 10 }.ToList(), list);
        }


        [TestMethod]
        public void MoveToEnd()
        {
            // Setup
            var list = new[] { "Apple", "Banana", "Carrot", "Egg Plant", "Fish Tacos" }.ToList();

            // Execute
            var result = list.MoveTo("Carrot", 4);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(new[] { "Apple", "Banana", "Egg Plant", "Fish Tacos", "Carrot" }.ToList(), list);
        }


        [TestMethod]
        public void MoveToWithNotExistingItem()
        {
            // Setup
            var list = new[] { "Apple", "Banana", "Carrot", "Egg Plant", "Fish Tacos" }.ToList();

            // Execute
            var result = list.MoveTo("Piano", 4);

            // Assert
            Assert.IsFalse(result);
            CollectionAssert.AreEqual(new[] { "Apple", "Banana", "Carrot", "Egg Plant", "Fish Tacos" }.ToList(), list);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MoveToNegativeIndex()
        {
            // Setup
            var list = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.ToList();

            // Execute
            list.MoveTo(4, -5);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MoveToOutOfRangeIndex()
        {
            // Setup
            var list = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.ToList();

            // Execute
            list.MoveTo(4, 50);
        }

      
      
      
    }
}
