using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinatorialAlgorithms
{
    class PermutationsIteratively
    {
        private static int countOfPerm = 0;
        static void Main()
        {
            Console.Write("Permutations without repetition for numbers from 1 up to: ");
            int n = int.Parse(Console.ReadLine());
            int[] permArr = Enumerable.Range(1, n).ToArray();
            int[] helper = Enumerable.Range(0, n + 1).ToArray();
            int index = 1;

            PrintPermutation(permArr);  

            while(index < n)
            {                              
                helper[index]--;
                int secondIndex = 0;

                if (index % 2 != 0)
                    secondIndex = helper[index];
                else
                    secondIndex = 0;

                Swap(ref permArr[secondIndex], ref permArr[index]);
                index = 1;

                while(helper[index] == 0)
                {
                    helper[index] = index;
                    index++;
                }

                PrintPermutation(permArr);  
            }

            Console.WriteLine("Overall permutations: " + countOfPerm);
        }

        private static void Swap(ref int val1, ref int val2)
        {
            int temp = val1;
            val1 = val2;
            val2 = temp;
        }

        private static void PrintPermutation(int[] permArr)
        {
            Console.WriteLine(string.Join(", ", permArr));
            countOfPerm++;
        }
    }
}
