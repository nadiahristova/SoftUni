using System;
using System.Collections.Generic;
using System.Linq;

   public class LinkedStack<T>
    {
        private class StackNode<T>
        {
            public T Value;

            public StackNode<T> PrevNode { get; set; }

            public StackNode(T value, StackNode<T> prevNode = null)
            {
                this.Value = value;
                this.PrevNode = prevNode;
            }
        }
        public int Count { get; private set; }

        private StackNode<T> firstOutElement;

        public void Push(T value)
        {
            if(this.Count == 0)
            {
                this.firstOutElement = new StackNode<T>(value);
            }
            else
            {
               this.firstOutElement = new StackNode<T>(value, this.firstOutElement);
            }

            this.Count++;
        }

        public T Pop()
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty!");
            }

            T retrievedValue = this.firstOutElement.Value;
            if (this.Count == 1)
                this.firstOutElement = null;
            else
            {
                retrievedValue = this.firstOutElement.Value;
                var currNode = this.firstOutElement.PrevNode;                
                this.firstOutElement.PrevNode = null;    //we can skip this step 
                this.firstOutElement = currNode;                          
            }

            this.Count--;
            return retrievedValue;
        }
        public T[] ToArray()
        {
            T[] outputArr = new T[this.Count];
            int arrInedx = 0;
            var currEl = this.firstOutElement;
            while (arrInedx != this.Count)
            {
                outputArr[arrInedx] = currEl.Value;
                currEl = currEl.PrevNode;
                arrInedx++;
            }

            return outputArr;
        }
    }
