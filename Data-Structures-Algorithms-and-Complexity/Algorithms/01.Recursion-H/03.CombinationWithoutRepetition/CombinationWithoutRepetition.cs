using System;
using System.Collections.Generic;
using System.Linq;

namespace Recursion_Homework
{
    class CombinationWithRepetition
    {
        static void Main()
        {
            Console.Write("n = ");
            int n = 5;// int.Parse(Console.ReadLine());
            Console.Write("k = ");
            int k = 3;// int.Parse(Console.ReadLine());
            int[] arr = new int[k];

            Console.WriteLine();

            CombineAndPrint(arr, 0, 1, n);
        }

        private static void CombineAndPrint(int[] arr, int index, int min, int max)
        {
            
            if (index == arr.Length)
            {
                PrintCombo(arr);
            }
            else
            {
                for (int i = min; i <= max; i++)
                {
                    arr[index] = i;
                    CombineAndPrint(arr, index + 1, i + 1, max);
                }                
            }
        }

        private static void PrintCombo(int[] arr)
        {
            Console.WriteLine("( " + string.Join(" ", arr) + " )");
        }
    }
}
