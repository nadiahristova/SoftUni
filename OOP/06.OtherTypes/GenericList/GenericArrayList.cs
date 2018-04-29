namespace GenericList
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    using Interfaces;
    using CustomExceptions;
    using System.Reflection;   

    [Version(2,11)]
    class GenericArrayList<T> : ICollection<T>, IGenericArrayList<T>
        where T : IComparable<T>
    {
        private const int DefaultCapacity = 16;

        private T[] array;
        private int Capacity { get { return this.array.Length; } }

        public GenericArrayList(int capacity = DefaultCapacity)
        {
            this.array = new T[capacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public bool IsReadOnly
        {
            get
            {
                return false; 
            }
        }

        /// <exception cref="EmptyListException">Will throw exception if list is empty</exception>
        /// <exception cref="IndexOutOfRangeException">Will throw exception if index is less than 0 or 
        /// greater than num of elements in the list</exception>
        public T this[int index]
        {
            get
            {
                this.IsListEmpty();
                this.IsValidIndex(index);

                return this.array[index];
            }
            set
            {
                this.IsValidIndex(index);

                this.array[index] = value;
            }
        }

        public void Add(T value)
        {
            if (array.Length <= this.Count)
                ResizeArray();

            this.array[this.Count] = value;
            this.Count++;
        }

        /// <exception cref="EmptyListException">Will throw exception if list is empty</exception>
        /// <exception cref="IndexOutOfRangeException">Will throw exception if index is less than 0 or 
        /// greater than num of elements in the list</exception>
        public T RemoveAt(int index)
        {
            this.IsListEmpty();
            this.IsValidIndex(index);

            T element = this[index];
            T[] intermediateArray = new T[this.Count - 1];

            Array.Copy(this.array, 0, intermediateArray, 0, index);
            Array.Copy(this.array, index + 1, intermediateArray, index, this.Count - index - 1);

            this.array = intermediateArray;
            this.Count--;

            return element;
        }

        /// <exception cref="IndexOutOfRangeException">Will throw exception if index is less than 0 or 
        /// greater than num of elements in the list</exception>
        public void Insert(T element, int index)
        {
            this.IsValidIndex(index);

            T[] intermediateArray = new T[this.Count + 1];
            Array.Copy(this.array, 0, intermediateArray, 0, index);
            intermediateArray[index] = element;
            Array.Copy(this.array, index, intermediateArray, index+1, this.Count - index);

            this.array = intermediateArray;
            this.Count++;
        }

        public void Clear()
        {
            this.array = new T[DefaultCapacity];
            this.Count = 0;
        }

        /// <summary>
        /// Finds the index of a given value.</summary>
        /// <returns>
        /// The index of the first encounter of a given value. In case the list does not 
        /// contain the value, the method returns -1.</returns>
        public int FindIndexByValue(T value)
        {
            for (int i = 0; i < this.Count; i++)
                if (this.array[i].CompareTo(value) == 0)
                    return i;

            return -1;
        }

        public bool Contains(T value)
        {
            return this.FindIndexByValue(value) == -1 ? false : true;
        }

        public bool Remove(T item)
        {
           int index = FindIndexByValue(item);

            if (index != -1)
            {
                this.RemoveAt(index);
                return true;
            }

            return false;     
        }

        public void RemoveAll(T item)
        {
            while (this.Contains(item))
            {
                int index = this.FindIndexByValue(item);

                if (index != -1)
                    this.RemoveAt(index);
            }            
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < this.Count; i++)
            {
                array[arrayIndex + i] = this.array[i];
            }
        }

        public void AddRange(IEnumerable<T> range)
        {
            foreach (var el in range)
                this.Add(el);
        }

        public static T Min<T>(GenericArrayList<T> list)
            where T :IComparable<T>
        {
            var minEl = list[0];

            for (int i = 1; i < list.Count; i++)
                if (list[i].CompareTo(minEl) < 0)
                    minEl = list[i];

            return minEl;
        }

        public static T Max<T>(GenericArrayList<T> list)
            where T : IComparable<T>
        {
            var maxEl = list[0];

            for (int i = 1; i < list.Count; i++)
                if (list[i].CompareTo(maxEl) > 0)
                    maxEl = list[i];

            return maxEl;
        }

        public static string Version()
        {
            Type currClassType = MethodBase.GetCurrentMethod().DeclaringType;

            var versionAttribute = currClassType
                .GetCustomAttributes(false).ToList()
                .FirstOrDefault(atr => atr is VersionAttribute);

            if (versionAttribute == null)
                throw new NotImplementedException("Class version was not implemented.");

            var va = versionAttribute as VersionAttribute;

            return $"{currClassType.Name.Substring(0, currClassType.Name.IndexOf('`'))}<{currClassType.GetGenericArguments()[0]}>'s Version: {va.Major}.{va.Minor}";
        }

        private void IsValidIndex(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new IndexOutOfRangeException("Max number of elements in the list are " 
                    + (this.Count - 1));
        }

        private void IsListEmpty()
        {
            if (this.Count == 0)//special case of IndexOutOfRangeException
                throw new EmptyListException("The list is empty. There are no elements to be removed.");
        }

        private void ResizeArray()
        {
            var newArray = new T[Count * 2];
            for (int i = 0; i < this.Count; i++)
                newArray[i] = array[i];

            this.array = newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            int currIndex = 0;
            while (currIndex != this.Count-1)
            {
                yield return array[currIndex];
                currIndex++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        => $"List members: {string.Join(", ", GenericArrayList<T>.SubArray(this.array, 0, this.Count))} " +
        $"\nElements count: {this.Count} \nList capacity: {this.Capacity}";

        private static T[] SubArray(T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }       
    }
}
