using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class TestSimpleLinkedList
    {

        public static void Main(string[] arg)
        {
            LinkedList<int> linky = new LinkedList<int>() { 0, 1, 2, 3, 4, 7, 5 };
            Console.WriteLine("Initail element's count: " + linky.Count);
            linky.Add(4); 
            Console.WriteLine("Element's count after adding one more: " + linky.Count);
            Console.WriteLine("We'll remove element with value " + linky.Remove(5) + " from the list");
            Console.WriteLine("Element's count after deleting one: " + linky.Count);
            Console.WriteLine("Index of the first occurance for element with value 4 is " + linky.FirstIndexOf(4));
            Console.WriteLine("Index of the last occurance for element with value 4 is " + linky.LastIndexOf(4));
            Console.WriteLine("Foreach for the Linked List:");
            Console.WriteLine();
            int index = 0;
            foreach (var item in linky)
            {
                Console.WriteLine(index + "th el : " + item + ";");
                index++;
            }
        }

    }
}
