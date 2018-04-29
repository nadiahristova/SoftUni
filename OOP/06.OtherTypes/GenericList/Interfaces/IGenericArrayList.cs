namespace GenericList.Interfaces
{
    using System.Collections.Generic;

    interface IGenericArrayList<T>
    {
        int Count { get; }

        T this[int index] { get; set; }

        bool IsReadOnly { get; }

        void Add(T value);

        T RemoveAt(int index);

        void Insert(T element, int index);

        void Clear();

        int FindIndexByValue(T value);

        bool Contains(T value);

        void AddRange(IEnumerable<T> range);

        string ToString();

        bool Remove(T item);

        void RemoveAll(T item);

        void CopyTo(T[] array, int arrayIndex);
    }
}
