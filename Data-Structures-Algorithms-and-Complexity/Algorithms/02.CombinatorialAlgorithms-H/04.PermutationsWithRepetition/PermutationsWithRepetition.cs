using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinatorialAlgorithms
{
    class PermutationsWithRepetition
    {
        private static int countOfPerm = 0;
        static void Main()
        {
            //Console.Write("Enter the number of integers in the array: ");
            //int n = int.Parse(Console.ReadLine());
            //int[] array = new int[n];
            //Console.WriteLine("Enter the integers(type 'exit' to dicontinue the insertion)");
            //int i = 0;
            
            //while (i < n)
            //{
            //    int num = int.Parse(Console.ReadLine().Trim());
            //    array[i] = num;
            //    i++;
            //}

            int[] array = { 1, 5, 5 };
            
            Array.Sort(array);
            permute(array, 0, array.Length - 1);

            Console.WriteLine("Overall permutations: " + countOfPerm);
        }
        public static void permute(int[] array, int start, int end)
        {
            PrintPerm(array);

            for (int left = end - 1; left >= start; left--)
            {
                for (int right = left + 1; right <= end; right++)
                {
                    if (array[left] != array[right])
                    {
                        Swap(ref array[left], ref array[right]);
                        permute(array, left + 1, end);
                    }
                }                    

                var firstElement = array[left];
                for (int i = left; i <= end - 1; i++)
                    array[i] = array[i + 1];
                array[end] = firstElement;
            }
        }
        private static void Swap(ref int val1, ref int val2)
        {
            int temp = val1;
            val1 = val2;
            val2 = temp;
        }
        private static void PrintPerm(int[] permArr)
        {
            Console.WriteLine(string.Join(", ", permArr));
            countOfPerm++;
        }
    }
}
