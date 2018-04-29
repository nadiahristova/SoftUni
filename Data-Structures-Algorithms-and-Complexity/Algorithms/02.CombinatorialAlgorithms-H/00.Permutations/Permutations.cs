using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinatorialAlgorithms
{
    class Permutations
    {
        private static int n = 5;
        private static int countOfPerm = 0;

        static void Main()
        {
            Console.Write("Permutations without repetition for numbers from 1 up to: ");
            n = int.Parse(Console.ReadLine());
            int[] array = Enumerable.Range(1, n).ToArray();
            Permutate(0, array);            
            Console.WriteLine("Overall permutations: " + countOfPerm);
        }

        private static void Permutate(int index, int[] array)
        {
            if(index == array.Length - 1)
            {
                Print(array);
                countOfPerm++;
            }
            else
            {
                for (var left = index; left < array.Length; left++)
                {
                    Swap(ref array[index], ref array[left]);
                    Permutate(index+1, array);
                    Swap(ref array[index], ref array[left]);
                }
            }
        }

        private static void Swap(ref int val1, ref int val2)
        {
            int temp = val1;
            val1 = val2;
            val2 = temp;

            //Variant with XOR - at present time much slower than using a normal temporary variable.
            //if(val1 == val2)
            //{
            //    return;
            //}

            //val1 ^= val2;
            //val2 ^= val1;
            //val1 ^= val2;
        }

        private static void Print(int[] array)
        {
            Console.WriteLine(string.Join(", ", array));
        }
    }
}
