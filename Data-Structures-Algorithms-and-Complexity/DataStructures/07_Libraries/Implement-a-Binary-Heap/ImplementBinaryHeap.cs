using System;
using System.Collections.Generic;

class ImplementBinaryHeap
{
    static void Main()
    {
        var priQueue = new PriorityQueue<int>();

        priQueue.Add(-5);
        priQueue.Add(7);
        priQueue.Add(9);
        priQueue.Add(5);
        priQueue.Add(1);
        priQueue.Add(8);

        priQueue.Remove();

        foreach (var el in priQueue)
        {
            Console.WriteLine(el);
        }
        Console.WriteLine("Element count: {0}, Capacity: {1}", priQueue.Count, priQueue.Capacity);

        priQueue.Clear();
        Console.WriteLine("Element count: {0}, Capacity: {1}", priQueue.Count, priQueue.Capacity);
    }
}

