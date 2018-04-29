using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class LinkedQueue<T>
    {
        private class QueueNode<T>
        {
            public T Value{ get; private set; }
            public QueueNode<T> NextNode { get; set; }
            public QueueNode<T> PrevNode { get; set; }
            public QueueNode(T value)
            {
                this.Value = value;
            }

        }

        private QueueNode<T> head;
        private QueueNode<T> tail;

        public int Count { get; private set; }

        public void Enqueue(T element)
        {
            if(this.Count == 0)
            {
                this.head = this.tail = new QueueNode<T>(element);
            }
            else
            {
                QueueNode<T> newHead = new QueueNode<T>(element);
                newHead.NextNode = this.head;
                this.head.PrevNode = newHead;
                this.head = newHead;
            }

            this.Count++;
        }

        public T Dequeue()
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException("Invalid Operation. The Queue is empty!");
            }

            T result = this.tail.Value;
            if(this.Count == 1)
            {
                this.tail = this.head = null;
            }
            else
            {
                this.tail = this.tail.PrevNode;
                this.tail.NextNode = null;
            }
                        
            this.Count--;

            return result;
        }

        public T[] ToArray()
        {
            T[] returnArray = new T[this.Count];
            var currNode = this.tail;

            for (int index = 0; index < this.Count; index++ )
            {
                returnArray[index] = currNode.Value;
                currNode = currNode.PrevNode;
            }

            return returnArray;
        }
    }

