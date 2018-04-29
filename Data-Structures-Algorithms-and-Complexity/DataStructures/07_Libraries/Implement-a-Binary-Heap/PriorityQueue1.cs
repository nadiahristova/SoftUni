using System;
using System.Collections;
using System.Collections.Generic;
internal class PriorityQueue1<T> : IEnumerable<T>
    where T : IComparable<T>
{
    private const int default_capacity = 5;
    private int capacity;
    private T[] data = new T[default_capacity];
    private int count;
    private T elInQuestion; // the element which will be moved around in the data structure
    private bool sorted;
    public int Count { get { return this.count; } }
    public int Capacity
    {
        get { return this.capacity; }
        set
        {
            int prevCapacity = capacity;
            capacity = Math.Max(value, prevCapacity);
            if (prevCapacity != capacity)
            {
                T[] helper = new T[capacity];
                Array.Copy(data, helper, count);
                data = helper;
            }
        }
    }

    public PriorityQueue1() 
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
        return (int)Math.Floor((index - 1) / 2.0);
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
        if (count == this.Capacity)
        {
            this.Capacity *= 2;
        }

        data[count] = item;
        this.elInQuestion = item;
        UpHeap(); //With this function we place the element in the last possible position in the T[] data 
        //then we slowly move it upwards until it's parent's value has higher priority than the new element
        count++;
        sorted = false;
    }
    public T Remove() //removes the root of the tree, takes the element with the lowest priority and place it at the beginning of the T[]
    //by placing the element with the lowest priority at the place of the highest the data structure finds again the item with the highest priority
    {
        if (this.count == 0)
        {
            throw new ArgumentException("Heap is empty.");
        }
        T item = data[0];
        count--;
        data[0] = data[count];
        data[count] = default(T);
        DownHeap();
        sorted = false;
        return item;
    }

    private void UpHeap()
    {
        int currPosition = count;
        int currParentIndex = ParentIndex(currPosition);

        while (currParentIndex > -1 && elInQuestion.CompareTo(data[currParentIndex]) < 0)
        {
            data[currPosition] = data[currParentIndex];
            currPosition = currParentIndex;
            currParentIndex = ParentIndex(currPosition);
        }
        data[currPosition] = elInQuestion;
    }

    private void DownHeap()
    {
        int currPosiotion = 0;
        int n; // possible new position of the current element
        this.elInQuestion = data[0];

        while (true)
        {
            int youngerChildIndex = YoungerChildIndex(currPosiotion);
            if (youngerChildIndex >= count) break; // the child with the higher prieority does not yet exists => we have reached the end of the data structure
            int olderChildIndex = OlderChildIndex(currPosiotion);
            if (olderChildIndex >= count)
                n = youngerChildIndex;
            else
                n = data[youngerChildIndex].CompareTo(data[olderChildIndex]) < 0 ? youngerChildIndex : olderChildIndex;

            if (elInQuestion.CompareTo(data[n]) > 0) // if the element is with lower prieority than the child with the higher priority for this position  
            {
                data[currPosiotion] = data[n]; //we move the child value to it's parent index
                currPosiotion = n;
            }
            else break; //there is no need for further reordering of the Heap
        }
        //until we find the right position for the movable element
        data[currPosiotion] = elInQuestion;
    }

    private void EnsureSort()
    {
        if (sorted) return;
        Array.Sort(data, 0, count);
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

