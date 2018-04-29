using System;
using System.Collections.Generic;
using System.Linq;

namespace Recursion_Homework
{
    class TowerOfHanoi
    {
        static Stack<int> source = new Stack<int>(Enumerable.Range(1, 3).Reverse());
        static Stack<int> destination = new Stack<int>();
        static Stack<int> spare = new Stack<int>();
        static int steps = 0;

        static void Main()
        {
            int bottomDisk = 3;//int.Parse(Console.ReadLine());
            MoveDisks(bottomDisk, source, destination, spare);
            Print();     
        }

        private static void Print()
        {
            if(steps != 0) Console.WriteLine(new String('-', 20));
            Console.WriteLine("\nSteps taken: {0}\n", steps);
            Console.WriteLine("Source: " + string.Join(", ", source));
            Console.WriteLine("Destination: " + string.Join(", ", destination));
            Console.WriteLine("Spare: " + string.Join(", ", spare));
            Console.WriteLine();
        }

        private static void MoveDisks(int bottomDisk, Stack<int> source, Stack<int> destination, Stack<int> spare)
        {
            Print();
            steps++;           

            if (bottomDisk == 1)
            {
                destination.Push(source.Pop());                
            }
            else
            {
                MoveDisks(bottomDisk - 1, source, spare, destination);      
                destination.Push(source.Pop());
                MoveDisks(bottomDisk - 1, spare, destination, source);
            }
        }
    }
}
