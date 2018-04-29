using System;
using System.Collections.Generic;
using System.Linq;

    public class ArrayStack<T>
    {
        private const int defaultCpacity = 16;
        private T[] elements;
        public int Count { get; private set; }

        public ArrayStack(int capacity = defaultCpacity)
        {
            this.elements = new T[capacity];
        }
        public void Push(T element)
        {
            if (this.Count == this.elements.Length)
                this.Grow();
            this.elements[this.Count] = element;
            this.Count++;
        }

        private void Grow()
        {
            T[] doubleArr = new T[this.Count * 2];
            Array.Copy(this.elements, doubleArr, this.elements.Length);
            this.elements = doubleArr;
        }

        public T Pop()
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty!");
            }
            T lastElement = this.elements[this.Count - 1];
            this.elements[this.Count - 1] = default(T);
            this.Count--;
            return lastElement;
        }

        public T[] ToArray()
        {
            T[] trimmedArray = new T[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                trimmedArray[i] = this.elements[this.Count - 1 - i];
            }            
            return trimmedArray;
        }
     
    }
