using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureReversedList
{
    class TestReversedList
    {
        public static void Main(string[] arg)
        {
            ReversedList<int> revList = new ReversedList<int>() {6, 5, 4, 3, 2, 1};
            revList.Add(0);

            Console.WriteLine("Element with index(0) is " + revList[0]);
            Console.WriteLine("Element with index(5) is " + revList[5]);
            Console.WriteLine();
            Console.WriteLine("Foreach for the Reversed List:");
            Console.WriteLine();
            int index = 0;
            foreach(var item in revList)
            {
                Console.WriteLine(index + "th el : " + item + ";");
                index++;
            }
            Console.WriteLine();
            Console.WriteLine("List Count: {0},\nList Capacity: {1}", revList.Count, revList.Capacity);
            Console.WriteLine();
            Console.WriteLine("Removing element with value 6 =>");
            revList.Remove(6);
            Console.WriteLine("List Count: {0},\nList Capacity: {1}", revList.Count, revList.Capacity);
        }
    }
}
