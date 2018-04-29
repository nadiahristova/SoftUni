using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSet
{
    class PlayWithOrderedSet
    {
        public static void Main()
        {
            OrderedSet<int> orderedSet = new OrderedSet<int>();
            orderedSet.Add(2);
            orderedSet.Add(-1);
            orderedSet.Add(5);
            orderedSet.Add(0);
            orderedSet.Add(555);
            orderedSet.Add(7);

            Console.WriteLine("Overall number of elements in the Ordered Set are: " + orderedSet.Count);
            Console.WriteLine("The elements of the Ordered Set are as follows:");
            orderedSet.PrintInOrder();

            Console.WriteLine("Does the Ordered Set countain node with value 7? " + orderedSet.Contains(7));
            Console.WriteLine("Is the value 2 removed from the Ordered Set? " + orderedSet.Remove(2));
            Console.WriteLine("Overall number of elements in the Ordered Set are: " + orderedSet.Count);
            Console.WriteLine("The elements of the Ordered Set are as follows:");

            foreach(var input in orderedSet)
            {
                Console.WriteLine(input);
            }
        }
    }
}
