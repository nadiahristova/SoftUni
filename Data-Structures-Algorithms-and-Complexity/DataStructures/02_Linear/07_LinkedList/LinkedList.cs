using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private class ListNode<T>
        {
            public T Value { get; private set; }
            public ListNode<T> NextNode { get; set; }
            public ListNode(T value)
            {
                this.Value = value;
            }
        }

        private ListNode<T> head;
        private ListNode<T> tail;

        public int Count { get; private set; }

        public void Add(T item)
        {
            if(this.Count == 0)
            {
                this.head = this.tail = new ListNode<T>(item);
            }
            else
            {
                var newItem = new ListNode<T>(item);
                this.tail.NextNode = newItem;
                this.tail = newItem;
            }
            this.Count++;
        }

        public T Remove(int index)
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The List is Empty");
            } 
            else
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException("Invalid index");
                }
            }

            T retVal = this.head.Value;

            if(index == 0)
            {
                this.head = this.head.NextNode;
            }
            else
            {
                int currIndex = 0;
                var currNode = this.head;

                while (currIndex != index-1)
                {
                    currIndex++;
                    currNode = currNode.NextNode;
                }
                retVal = currNode.NextNode.Value;
                currNode.NextNode = currNode.NextNode.NextNode;
                if (index == this.Count - 1)
                {
                    this.tail = currNode;
                }
            }
            this.Count--;
            return retVal;
        }

        public int FirstIndexOf(T item)
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The List is Empty");
            }
           
            int returnIndex = -1;
            int currIndex = 0;
            var currNode = this.head;

            while (!EqualityComparer<T>.Default.Equals(currNode.Value, item) && currIndex < this.Count)
            {
                currIndex++;
                currNode = currNode.NextNode;
            }

            if (currIndex != this.Count)
            {
                returnIndex = currIndex;
            }
                        
            return returnIndex;
        }

        public int LastIndexOf(T item)
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The List is Empty");
            }

            int returnIndex = -1;
            int currIndex = 0;
            var currNode = this.head;

            while (currIndex < this.Count)
            {
                if (EqualityComparer<T>.Default.Equals(currNode.Value, item))
                {
                    returnIndex = currIndex;
                }
                currIndex++;
                currNode = currNode.NextNode;
            }

            return returnIndex;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
