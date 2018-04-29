using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureReversedList
{
    class ReversedList<T> : IEnumerable<T>
    {
        private const int defaultCapacity = 5;
        private T[] listData;
        public int Count { get; private set; }
        public int Capacity
        {
            get
            {
                return this.listData.Length;
            }
        }
        
        public ReversedList(int capacity = defaultCapacity)
        {
            listData = new T[capacity];
        }

        private void IsValidIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException("No such element as >> " + index + " << in the Reversed List.");
            }
        }

        public T this[int index]
        {
            get
            {
                this.IsValidIndex(index);
                return this.listData[this.Count - index - 1];
            }
            set
            {
                this.IsValidIndex(index);
                this.listData[this.Count - index - 1] = value;
            }            
        }

        private void DoubleCapacity()
        {
            Array.Resize<T>(ref listData, this.Capacity * 2);
        }
        public void Add(T item)
        {
            if(this.Count >= listData.Length)
            {
                this.DoubleCapacity();
            }

            listData[this.Count] = item;
            this.Count++;
        }

        public void Remove(int index)
        {
            this.IsValidIndex(index);

            T[] intermediateArray = new T[this.Count-1];
            Array.Copy(this.listData, 0, intermediateArray, 0, index);
            Array.Copy(this.listData, index+1, intermediateArray, index, this.Count - index - 1);
            this.listData = intermediateArray;
            this.Count--;
        }

       public void Clear()
        {
            listData = new T[defaultCapacity];
            this.Count = 0;
        }

       public IEnumerator<T> GetEnumerator()
       {
           int currIndex = this.Count -1;
           while (currIndex != -1)
           {
               yield return listData[currIndex];
               currIndex--;
           }
       }

       IEnumerator IEnumerable.GetEnumerator()
       {
           return this.GetEnumerator();
       }
    }
}
