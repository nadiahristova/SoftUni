using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinatorialAlgorithms
{
    class CombinationsIteratively
    {
        private static int countOfCombo = 0;
        static void Main()
        {
            Console.Write("Combinations without repetition for numbers from 1 up to: ");
            int numElements = int.Parse(Console.ReadLine());
            Console.Write("out of groups of: ");
            int numGroup = int.Parse(Console.ReadLine());

            int[] combination = Enumerable.Range(1, numGroup).ToArray();
 
            PrintCombo(combination);
            while (calcNextCombo(combination, numGroup, numElements))
            {
                PrintCombo(combination);
            }

            Console.WriteLine("Overall combinations: " + countOfCombo);
        }

        private static bool calcNextCombo(int[] combo, int k, int n) {
            int index = k - 1;
            combo[index]++;
            while ((index > 0) && (combo[index] >= n - k + 2 + index)) {
                index--;
                combo[index]++;
            }
 
            if (combo[0] > n + 1 - k) 
                return false; 
                 
            for (int i = index + 1; i < k; i++)
                combo[i] = combo[i - 1] + 1;
 
            return true;
        }

        private static void PrintCombo(int[] comboArr)
        {
            Console.WriteLine(string.Join(", ", comboArr));
            countOfCombo++;
        }
    }
}
