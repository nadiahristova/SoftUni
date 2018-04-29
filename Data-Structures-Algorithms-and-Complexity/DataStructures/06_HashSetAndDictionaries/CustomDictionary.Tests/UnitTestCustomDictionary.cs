using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomDictionary;
using System.Collections.Generic;
using System.Linq;

namespace CustomDictionary
{
    [TestClass]
    public class UnitTestCustomDictionary
    {
        [TestMethod]
        public void Add_EmptyHashTable_NoDuplicates_ShouldAddElement()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, int>();

            // Act
            var elements = new KeyValue<string, int>[]
            {
                new KeyValue<string, int>("Peter", 5), 
                new KeyValue<string, int>("Maria", 6), 
                new KeyValue<string, int>("George", 4), 
                new KeyValue<string, int>("Kiril", 5)
            };
            foreach (var element in elements)
            {
                customDictionary.Add(element.Key, element.Value);
            }

            // Assert
            var actualElements = customDictionary.ToList();
            CollectionAssert.AreEquivalent(elements, actualElements);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_EmptyHashTable_Duplicates_ShouldThrowException()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, string>();

            // Act
            customDictionary.Add("Peter", "first");
            customDictionary.Add("Peter", "second");
        }

        [TestMethod]
        public void Add_1000_Elements_Grow_ShouldWorkCorrectly()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, int>(1);

            // Act
            var expectedElements = new List<KeyValue<string, int>>();
            for (int i = 0; i < 1000; i++)
            {
                customDictionary.Add("key" + i, i);
                expectedElements.Add(new KeyValue<string, int>("key" + i, i));
            }

            // Assert
            var actualElements = customDictionary.ToList();
            CollectionAssert.AreEquivalent(expectedElements, actualElements);
        }

        [TestMethod]
        public void AddOrReplace_WithDuplicates_ShouldWorkCorrectly()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, int>();

            // Act
            customDictionary.AddOrReplace("Peter", 555);
            customDictionary.AddOrReplace("Maria", 999);
            customDictionary.AddOrReplace("Maria", 123);
            customDictionary.AddOrReplace("Maria", 6);
            customDictionary.AddOrReplace("Peter", 5);

            // Assert
            var expectedElements = new KeyValue<string, int>[]
            {
                new KeyValue<string, int>("Peter", 5), 
                new KeyValue<string, int>("Maria", 6)
            };
            var actualElements = customDictionary.ToList();
            CollectionAssert.AreEquivalent(expectedElements, actualElements);
        }

        [TestMethod]
        public void Count_Empty_Add_Remove_ShouldWorkCorrectly()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, int>();

            // Assert
            Assert.AreEqual(0, customDictionary.Count);

            // Act & Assert
            customDictionary.Add("Peter", 555);
            customDictionary.AddOrReplace("Peter", 555);
            customDictionary.AddOrReplace("Ivan", 555);
            Assert.AreEqual(2, customDictionary.Count);

            // Act & Assert
            customDictionary.Remove("Peter");
            Assert.AreEqual(1, customDictionary.Count);

            // Act & Assert
            customDictionary.Remove("Peter");
            Assert.AreEqual(1, customDictionary.Count);

            // Act & Assert
            customDictionary.Remove("Ivan");
            Assert.AreEqual(0, customDictionary.Count);
        }

        [TestMethod]
        public void Get_ExistingElement_ShouldReturnTheValue()
        {
            // Arrange
            var customDictionary = new CustomDictionary<int, string>();

            // Act
            customDictionary.Add(555, "Peter");
            var actualValue = customDictionary.Get(555);

            // Assert
            Assert.AreEqual("Peter", actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Get_NonExistingElement_ShouldThrowException()
        {
            // Arrange
            var customDictionary = new CustomDictionary<int, string>();

            // Act
            customDictionary.Get(12345);
        }

        [TestMethod]
        public void Indexer_ExistingElement_ShouldReturnTheValue()
        {
            // Arrange
            var customDictionary = new CustomDictionary<int, string>();

            // Act
            customDictionary.Add(555, "Peter");
            var actualValue = customDictionary[555];

            // Assert
            Assert.AreEqual("Peter", actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Indexer_NonExistingElement_ShouldThrowException()
        {
            // Arrange
            var customDictionary = new CustomDictionary<int, string>();

            // Act
            var value = customDictionary[12345];
        }

        [TestMethod]
        public void Indexer_AddReplace_WithDuplicates_ShouldWorkCorrectly()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, int>();

            // Act
            customDictionary["Peter"] = 555;
            customDictionary["Maria"] = 999;
            customDictionary["Maria"] = 123;
            customDictionary["Maria"] = 6;
            customDictionary["Peter"] = 5;

            // Assert
            var expectedElements = new KeyValue<string, int>[]
        {
            new KeyValue<string, int>("Peter", 5), 
            new KeyValue<string, int>("Maria", 6)
        };
            var actualElements = customDictionary.ToList();
            CollectionAssert.AreEquivalent(expectedElements, actualElements);
        }

        [TestMethod]
        public void Capacity_Grow_ShouldWorkCorrectly()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, int>(2);

            // Assert
            Assert.AreEqual(2, customDictionary.Capacity);

            // Act
            customDictionary.Add("Peter", 123);
            customDictionary.Add("Maria", 456);

            // Assert
            Assert.AreEqual(4, customDictionary.Capacity);

            // Act
            customDictionary.Add("Tanya", 555);
            customDictionary.Add("George", 777);

            // Assert
            Assert.AreEqual(8, customDictionary.Capacity);
        }

        [TestMethod]
        public void TryGetValue_ExistingElement_ShouldReturnTheValue()
        {
            // Arrange
            var customDictionary = new CustomDictionary<int, string>();

            // Act
            customDictionary.Add(555, "Peter");
            string value;
            var result = customDictionary.TryGetValue(555, out value);

            // Assert
            Assert.AreEqual("Peter", value);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TryGetValue_NonExistingElement_ShouldReturnFalse()
        {
            // Arrange
            var customDictionary = new CustomDictionary<int, string>();

            // Act
            string value;
            var result = customDictionary.TryGetValue(555, out value);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Find_ExistingElement_ShouldReturnTheElement()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, DateTime>();
            var name = "Maria";
            var date = new DateTime(1995, 7, 18);
            customDictionary.Add(name, date);

            // Act
            var element = customDictionary.Find(name);

            // Assert
            var expectedElement = new KeyValue<string, DateTime>(name, date);
            Assert.AreEqual(expectedElement, element);
        }

        [TestMethod]
        public void Find_NonExistingElement_ShouldReturnNull()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, DateTime>();

            // Act
            var element = customDictionary.Find("Maria");

            // Assert
            Assert.IsNull(element);
        }

        [TestMethod]
        public void ContainsKey_ExistingElement_ShouldReturnTrue()
        {
            // Arrange
            var customDictionary = new CustomDictionary<DateTime, string>();
            var date = new DateTime(1995, 7, 18);
            customDictionary.Add(date, "Some value");

            // Act
            var containsKey = customDictionary.ContainsKey(date);

            // Assert
            Assert.IsTrue(containsKey);
        }

        [TestMethod]
        public void ContainsKey_NonExistingElement_ShouldReturnFalse()
        {
            // Arrange
            var customDictionary = new CustomDictionary<DateTime, string>();
            var date = new DateTime(1995, 7, 18);

            // Act
            var containsKey = customDictionary.ContainsKey(date);

            // Assert
            Assert.IsFalse(containsKey);
        }

        [TestMethod]
        public void Remove_ExistingElement_ShouldWorkCorrectly()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, double>();
            customDictionary.Add("Peter", 12.5);
            customDictionary.Add("Maria", 99.9);

            // Assert
            Assert.AreEqual(2, customDictionary.Count);

            // Act
            var removed = customDictionary.Remove("Peter");

            // Assert
            Assert.IsTrue(removed);
            Assert.AreEqual(1, customDictionary.Count);
        }

        [TestMethod]
        public void Remove_NonExistingElement_ShouldWorkCorrectly()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, double>();
            customDictionary.Add("Peter", 12.5);
            customDictionary.Add("Maria", 99.9);

            // Assert
            Assert.AreEqual(2, customDictionary.Count);

            // Act
            var removed = customDictionary.Remove("George");

            // Assert
            Assert.IsFalse(removed);
            Assert.AreEqual(2, customDictionary.Count);
        }

        [TestMethod]
        public void Remove_5000_Elements_ShouldWorkCorrectly()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, int>();
            var keys = new List<string>();
            var count = 5000;
            for (int i = 0; i < count; i++)
            {
                var key = Guid.NewGuid().ToString();
                keys.Add(key);
                customDictionary.Add(key, i);
            }

            // Assert
            Assert.AreEqual(count, customDictionary.Count);

            // Act & Assert
            keys.Reverse();
            foreach (var key in keys)
            {
                customDictionary.Remove(key);
                count--;
                Assert.AreEqual(count, customDictionary.Count);
            }

            // Assert
            var expectedElements = new List<KeyValue<string, int>>();
            var actualElements = customDictionary.ToList();
            CollectionAssert.AreEquivalent(expectedElements, actualElements);
        }

        [TestMethod]
        public void Clear_ShouldWorkCorrectly()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, int>();

            // Assert
            Assert.AreEqual(0, customDictionary.Count);

            // Act
            customDictionary.Clear();

            // Assert
            Assert.AreEqual(0, customDictionary.Count);

            // Arrange
            customDictionary.Add("Peter", 5);
            customDictionary.Add("George", 7);
            customDictionary.Add("Maria", 3);

            // Assert
            Assert.AreEqual(3, customDictionary.Count);

            // Act
            customDictionary.Clear();

            // Assert
            Assert.AreEqual(0, customDictionary.Count);
            var expectedElements = new List<KeyValue<string, int>>();
            var actualElements = customDictionary.ToList();
            CollectionAssert.AreEquivalent(expectedElements, actualElements);

            customDictionary.Add("Peter", 5);
            customDictionary.Add("George", 7);
            customDictionary.Add("Maria", 3);

            // Assert
            Assert.AreEqual(3, customDictionary.Count);
        }

        [TestMethod]
        public void Keys_ShouldWorkCorrectly()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, double>();

            // Assert
            CollectionAssert.AreEquivalent(new string[0], customDictionary.Keys.ToArray());

            // Arrange
            customDictionary.Add("Peter", 12.5);
            customDictionary.Add("Maria", 99.9);
            customDictionary["Peter"] = 123.45;

            // Act
            var keys = customDictionary.Keys;

            // Assert
            var expectedKeys = new string[] { "Peter", "Maria" };
            CollectionAssert.AreEquivalent(expectedKeys, keys.ToArray());
        }

        [TestMethod]
        public void Values_ShouldWorkCorrectly()
        {
            // Arrange
            var customDictionary = new CustomDictionary<string, double>();

            // Assert
            CollectionAssert.AreEquivalent(new string[0], customDictionary.Values.ToArray());

            // Arrange
            customDictionary.Add("Peter", 12.5);
            customDictionary.Add("Maria", 99.9);
            customDictionary["Peter"] = 123.45;

            // Act
            var values = customDictionary.Values;

            // Assert
            var expectedValues = new double[] { 99.9, 123.45 };
            CollectionAssert.AreEquivalent(expectedValues, values.ToArray());
        }
    }
}
