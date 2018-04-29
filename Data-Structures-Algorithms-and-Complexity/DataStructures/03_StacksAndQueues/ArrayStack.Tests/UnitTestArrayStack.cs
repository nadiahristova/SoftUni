using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArrayBasedStack
{
    [TestClass]
    public class UnitTestArrayStack
    {
        [TestMethod]
        public void Push_EmptyStack_ShouldAddElement()
        {
            //Arrange
            var stack = new ArrayStack<int>();

            //Assert count of an empty Stack
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
            var stack = new ArrayStack<string>();
            var elementToBeAdded = "An element";

            //Act
            stack.Push(elementToBeAdded);
            var removedElement = stack.Pop();

            //Assert
            Assert.AreEqual(0, stack.Count);
            Assert.AreEqual(elementToBeAdded, removedElement);
        }

        [TestMethod]
        public void PushAndPop_StackWith1000Elemnts_ShouldTestTheAutoGrowFunctionality()
        {
            //Arrange
            var stack = new ArrayStack<string>();
            int i = 0;

            //Assert
            Assert.AreEqual(0, stack.Count);

            
            while(i != 1000)
            {
                //Act
                i++;
                stack.Push(i.ToString());
                
                //Assert
                Assert.AreEqual(i, stack.Count);
            }

            while (i != 0)
            {
                //Act
                i--;
                string lastElement = stack.Pop();

                //Assert
                Assert.AreEqual(i, stack.Count);
                Assert.AreEqual((i + 1).ToString(), lastElement);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_EmptyStack_ThrowsAnException()
        {
            //Arrange
            var stack = new ArrayStack<int>();

            //Act
            stack.Pop();

            //Assert: expect an exception
        }

        [TestMethod]
        public void PushAndPop_StackWithInitialCapacity1_ShouldWorkCorrectly()
        {
            //Arrange
            var stack = new ArrayStack<int>(1);

            //Assert
            Assert.AreEqual(0, stack.Count);

            //Act
            stack.Push(0);

            //Assert
            Assert.AreEqual(1, stack.Count);

            //Act
            stack.Push(1);

            //Assert
            Assert.AreEqual(2, stack.Count);

            //Act
            var secondElement = stack.Pop();

            //Assert
            Assert.AreEqual(1, secondElement);
            Assert.AreEqual(1, stack.Count);

            //Act
            var firstElement = stack.Pop();

            //Assert
            Assert.AreEqual(0, firstElement);
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void Push4Elements_ToArray_ShouldWorkCorrectly()
        {
            //Arrange
            var array = new int[] { 3, 5, -2, 7};
            var stack = new ArrayStack<int>();

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

}