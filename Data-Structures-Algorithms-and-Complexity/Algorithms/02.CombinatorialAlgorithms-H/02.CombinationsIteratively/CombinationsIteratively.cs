using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinatorialAlgorithms
{
    //all credits to https://github.com/ValentinAlexandrovGeorgiev/AlgorithmsCourse/blob/master/2.%20Combinatorial%20Algorithms/HomeWork/CombinationsIteratively/GenerateCombinationsIteratively.cs
    class CombinationsIteratively
    {
        private static int countOfCombo = 0;
        static void Main()
        {
            Console.Write("Combinations without repetition for numbers from 1 up to: ");
            int numElements = int.Parse(Console.ReadLine());
            Console.Write("out of groups of: ");
            int numGroup = int.Parse(Console.ReadLine());

            Stack<int> helper = new Stack<int>();

            int[] combination = new int[numGroup];
            helper.Push(1);

            while (helper.Count > 0)
            {
                int index = helper.Count - 1;
                int value = helper.Pop();

                while (value <= numElements)
                {
                    combination[index] = value;
                    index++;
                    value++;
                    helper.Push(value);
                    if (index == numGroup)
                    {
                        PrintCombo(combination);
                        break;
                    }
                }
            }

            Console.WriteLine("Overall combinations: " + countOfCombo);
        }

        private static void PrintCombo(int[] permArr)
        {
            Console.WriteLine(string.Join(", ", permArr));
            countOfCombo++;
        }
    }
}
