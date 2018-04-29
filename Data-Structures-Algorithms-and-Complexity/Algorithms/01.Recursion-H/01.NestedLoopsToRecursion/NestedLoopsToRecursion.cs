using System;
using System.Linq;
using System.Collections.Generic;

namespace Recursion_Homework
{
    class NestedLoopsToRecursion
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int[] arr = new int[n];
            CombineAndPrint(arr, 0);
        }
      

        private static void CombineAndPrint(int[] arr, int index)
        {
            if (index == arr.Length)
            {
                PrintCombo(arr);
            }
            else 
            {
                for (int i = 1; i <= arr.Length; i++)
                {
                    arr[index] = i;
                    CombineAndPrint(arr, index + 1);
                }
            }
        }

        private static void PrintCombo(int[] arr)
        {
            Console.WriteLine(string.Join(" ", arr));
        }
    }
}
