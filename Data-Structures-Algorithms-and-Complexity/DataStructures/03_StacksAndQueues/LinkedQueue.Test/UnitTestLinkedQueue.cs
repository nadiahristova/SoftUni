using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


    [TestClass]
    public class UnitTestLinkedQueue
    {
        [TestMethod]
        public void Enqueue_EmptyQueue_ShouldAddElement()
        {
            //Arrange
            var queue = new LinkedQueue<int>();

            //Assert count of an empty Queue
            Assert.AreEqual(0, queue.Count);

            //Act
            queue.Enqueue(5);

            //Assert
            Assert.AreEqual(1, queue.Count);
        }


        [TestMethod]
        public void EnqueueAndDequeue_QueueWith1Element_ShouldRemoveElement()
        {
            //Arrange
            var queue = new LinkedQueue<string>();
            var elementToBeAdded = "An element";

            //Act
            queue.Enqueue(elementToBeAdded);
            var removedElement = queue.Dequeue();

            //Assert
            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual(elementToBeAdded, removedElement);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dequeue_EmptyQueue_ThrowsAnException()
        {
            //Arrange
            var queue = new LinkedQueue<int>();

            //Act
            queue.Dequeue();

            //Assert: expect an exception
        }
           
        [TestMethod]
        public void Enqueue4Elements_ToArray_ShouldWorkCorrectly()
        {
            //Arrange
            var array = new int[] { 3, 5, -2, 7 };
            var queue = new LinkedQueue<int>();

            //Act
            for (int i = 0; i < array.Length; i++)
            {
                queue.Enqueue(array[i]);
            }
            var arrayFromStack = queue.ToArray();

            //Assert
            CollectionAssert.AreEqual(array, arrayFromStack);
        }
    }
