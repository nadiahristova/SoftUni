using System;
using System.Collections.Generic;
using System.Linq;

namespace Recursion_Homework
{
    class CombinationsWithRepetitions
    {
        static LinkedList<int> possElements;
        static void Main()
        {
            Console.Write("n = ");
            int n = 5;// int.Parse(Console.ReadLine());
            Console.Write("k = ");
            int k = 3;// int.Parse(Console.ReadLine());
            int[] arr = new int[k];
            possElements = new LinkedList<int>(Enumerable.Range(1, n).Reverse());

            Console.WriteLine();

            CombineAndPrint(arr, 0, n);
        }

        private static void CombineAndPrint(int[] arr, int index, int max)
        {
            if (index == arr.Length)
            {
                PrintCombo(arr);
            }
            else
            {
                for (int i = 1; i <= max; i++)
                {
                    if (possElements.Last() <= i)
                    {
                        arr[index] = i;
                        CombineAndPrint(arr, index + 1,  max);
                    }
                    if (i == max)
                    {
                        int el = possElements.Last();
                        possElements.RemoveLast();
                        possElements.AddFirst(el);
                    }

                    if (index == 0)
                    {
                        possElements.RemoveFirst();
                    }
                }                
            }
        }

        private static void PrintCombo(int[] arr)
        {
            Console.WriteLine("( " + string.Join(" ", arr) + " )");
        }
    }
}
