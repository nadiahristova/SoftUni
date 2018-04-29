using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderedSet;
using System.Collections.Generic;

namespace OrderedSet
{
    [TestClass]
    public class UnitTestOrderedSet
    {
        [TestMethod]
        public void Add_EmptyOrderedSet_NoDuplicates_ShouldAddElement()
        {
            // Arrange
            var orderedSet = new OrderedSet<int>();

            // Act
            var elements = new List<int> {-5, 16, 0, 555};
            foreach (var element in elements)
            {
                orderedSet.Add(element);
            }
            elements.Sort();
            // Assert
            var actualElements = orderedSet.ToList();
            CollectionAssert.AreEquivalent(elements, actualElements);
        }

        [TestMethod]
        public void Add_1000_Elements_Grow_ShouldWorkCorrectly()
        {
            // Arrange
            var orderedSet = new OrderedSet<int>();

            // Act
            var expectedElements = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                orderedSet.Add(i);
                expectedElements.Add(i);
            }

            // Assert
            var actualElements = orderedSet.ToList();
            Assert.AreEqual(expectedElements.Count, actualElements.Count);
        }

        [TestMethod]
        public void Count_Empty_Add_Remove_ShouldWorkCorrectly()
        {
            // Arrange
            var orderedSet = new OrderedSet<int>();

            // Assert
            Assert.AreEqual(0, orderedSet.Count);

            // Act & Assert
            orderedSet.Add(555);
            orderedSet.Add(-555);
            orderedSet.Add(555);
            Assert.AreEqual(2, orderedSet.Count);

            // Act & Assert
            bool removed = orderedSet.Remove(-555);
            Assert.AreEqual(1, orderedSet.Count);
            Assert.IsTrue(removed);

            // Act & Assert
            removed = orderedSet.Remove(-555);
            Assert.AreEqual(1, orderedSet.Count);
            Assert.IsFalse(removed);

            // Act & Assert
            removed = orderedSet.Remove(555);
            Assert.AreEqual(0, orderedSet.Count);
            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void Contains_ExistingElement_ShouldReturnTrue()
        {
            // Arrange
            var orderedSet = new OrderedSet<int>();
            orderedSet.Add(1);

            // Act
            var contains = orderedSet.Contains(1);

            // Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void Contains_NonExistingElement_ShouldReturnFalse()
        {
            // Arrange
            var orderedSet = new OrderedSet<int>();
            orderedSet.Add(5);

            // Act
            var contains = orderedSet.Contains(1);

            // Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void Remove_ExistingElement_ShouldWorkCorrectly()
        {
            // Arrange
            var orderedSet = new OrderedSet<int>();
            orderedSet.Add(12);
            orderedSet.Add(99);

            // Assert
            Assert.AreEqual(2, orderedSet.Count);

            // Act
            var removed = orderedSet.Remove(12);

            // Assert
            Assert.IsTrue(removed);
            Assert.AreEqual(1, orderedSet.Count);
        }

        [TestMethod]
        public void Remove_NonExistingElement_ShouldWorkCorrectly()
        {
            // Arrange
            var orderedSet = new OrderedSet<int>();
            orderedSet.Add(12);
            orderedSet.Add(99);

            // Assert
            Assert.AreEqual(2, orderedSet.Count);

            // Act
            var removed = orderedSet.Remove(21);

            // Assert
            Assert.IsFalse(removed);
            Assert.AreEqual(2, orderedSet.Count);
        }

        public void Remove_5000_Elements_ShouldWorkCorrectly()
        {
            // Arrange
            var orderedSet = new OrderedSet<int>();
            var listInt = new List<int>();
            var count = 5000;
            for (int i = 0; i < count; i++)
            {                
                listInt.Add(i);
                orderedSet.Add(i);
            }

            // Assert
            Assert.AreEqual(count, orderedSet.Count);

            // Act & Assert            
            foreach (var i in listInt)
            {
                orderedSet.Remove(i);
                count--;
                Assert.AreEqual(count, orderedSet.Count);
            }

            // Assert
            var expectedElements = new List<int>();
            var actualElements = orderedSet.ToList();
            CollectionAssert.AreEquivalent(expectedElements, actualElements);
        }

        public void Clear_ShouldWorkCorrectly()
        {
            // Arrange
            var orderedSet = new OrderedSet<int>();

            // Assert
            Assert.AreEqual(0, orderedSet.Count);

            // Act
            orderedSet.Clear();

            // Assert
            Assert.AreEqual(0, orderedSet.Count);

            // Arrange
            orderedSet.Add(5);
            orderedSet.Add(7);
            orderedSet.Add(3);

            // Assert
            Assert.AreEqual(3, orderedSet.Count);

            // Act
            orderedSet.Clear();

            // Assert
            Assert.AreEqual(0, orderedSet.Count);
        }
    }
}
