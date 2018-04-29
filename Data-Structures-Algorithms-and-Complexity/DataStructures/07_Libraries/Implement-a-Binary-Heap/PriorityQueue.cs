using System;
using System.Collections;
using System.Collections.Generic;
internal class PriorityQueue<T> : IEnumerable<T>
    where T : IComparable<T>
{
    private const int default_capacity = 5;
    private int capacity;
    private T[] data = new T[default_capacity];
    private int count;
    private T elInQuestion; 
    private bool sorted;
    public int Count { get { return this.count; } }
    public int Capacity
    {
        get { return this.capacity; }
        set
        {
            int prevCapacity = capacity;
            capacity = Math.Max(value, prevCapacity);
            if(prevCapacity != capacity)
            {
                T[] helper = new T[capacity];
                Array.Copy(data, helper, count);
                data = helper;
            }
        }
    }

    public PriorityQueue() 
    {
        this.Capacity = default_capacity;
    }
   
    public T Peek()
    {
        return this.data[0];
    }
    public void Clear()
    {
        this.data = new T[default_capacity];
        this.count = 0;
        capacity = default_capacity;
    }
    private static int ParentIndex(int index)
    {
        return (int)Math.Floor((index-1)/2.0);
    }
    private static int YoungerChildIndex(int index)
    {
        return index * 2 + 1;
    }
    private static int OlderChildIndex(int index)
    {
        return index * 2 + 2;
    }

    public void Add(T item)
    {
        if(count == this.Capacity)
        {
            this.Capacity *= 2;
        }

        data[count] = item;
        this.elInQuestion = item;
        UpHeap(count); 
        count++;
    }
    public T Remove() 
    {
        if(this.count == 0)
        {
            throw new ArgumentException("Heap is empty.");
        }
        T item = data[0];
        count--;
        data[0] = data[count];
        data[count] = default(T);
        DownHeap(0);
        return item;
    }

    private void UpHeap(int currPosition)
    {
        int currParentIndex = ParentIndex(currPosition);

        while(currParentIndex > -1 && elInQuestion.CompareTo(data[currParentIndex]) < 0)
        {
            T parentEl = data[currParentIndex];
            data[currParentIndex] = data[currPosition];
            data[currPosition] = parentEl;
            UpHeap(currParentIndex);
        }
    }

    private void DownHeap(int currPosiotion)
    {        
        int youngerChildIndex = YoungerChildIndex(currPosiotion);        
        int olderChildIndex = OlderChildIndex(currPosiotion);
        int n = currPosiotion;

        if (youngerChildIndex < count && data[youngerChildIndex].CompareTo(data[n]) < 0)
            n = youngerChildIndex;
        if (olderChildIndex < count && data[olderChildIndex].CompareTo(data[n]) < 0)
            n = olderChildIndex;
        if(n != currPosiotion)
        {
            T parentEl = data[currPosiotion];
            data[currPosiotion] = data[n];
            data[n] = parentEl;
            DownHeap(n);
        }
    }

    private void EnsureSort()
    {
        if (sorted) return;
        Array.Sort(data, 0 , count);
        sorted = true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        EnsureSort();
        for (int i = 0; i < count; i++)
        {
            yield return data[i];
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

