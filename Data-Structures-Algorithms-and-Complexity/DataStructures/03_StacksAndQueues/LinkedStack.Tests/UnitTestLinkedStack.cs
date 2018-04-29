using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


    [TestClass]
    public class UnitTestLinkedStack
    {
        [TestMethod]
        public void Push_EmptyStack_ShouldAddElement()
        {
            //Arrange
            var stack = new LinkedStack<int>();

            //Assert 
            Assert.AreEqual(0, stack.Count);

            //Act
            stack.Push(5);

            //Assert
            Assert.AreEqual(1, stack.Count);
        }

        [TestMethod]
        public void PushAndPop_StackWith1Element_ShouldRemoveElement()
        {
            //Arrange
            var stack = new LinkedStack<string>();
            var elementToBeAdded = "An element";

            //Act
            stack.Push(elementToBeAdded);

            //Assert
            Assert.AreEqual(1, stack.Count);

            //Act
            var removedElement = stack.Pop();

            //Assert
            Assert.AreEqual(0, stack.Count);
            Assert.AreEqual(elementToBeAdded, removedElement);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_EmptyStack_ThrowsAnException()
        {
            //Arrange
            var stack = new LinkedStack<int>();

            //Act
            stack.Pop();

            //Assert: expect an exception
        }

        [TestMethod]
        public void Push4Elements_ToArray_ShouldWorkCorrectly()
        {
            //Arrange
            var array = new int[] { 3, 5, -2, 7 };
            var stack = new LinkedStack<int>();

            //Act
            for (int i = 0; i < array.Length; i++)
            {
                stack.Push(array[i]);
            }
            var arrayFromStack = stack.ToArray();
            Array.Reverse(array);

            //Assert
            CollectionAssert.AreEqual(array, arrayFromStack);
        }
    }

