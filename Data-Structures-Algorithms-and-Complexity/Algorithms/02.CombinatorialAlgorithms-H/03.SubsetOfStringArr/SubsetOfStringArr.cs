using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinatorialAlgorithms
{
    class SubsetOfStringArr // we are searching for combinations without repetitions
    {
        private static int countOfPerm = 0;
        private static List<string> stringSet = new List<string>();
        static void Main()
        {
            Console.Write("Number of subset strings: ");
            int k = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the strings(type 'exit' to dicontinue the insertion)");
            string line = Console.ReadLine().Trim();
            while (line.Trim().ToLower() != "exit")
            {
                stringSet.Add(line);
                line = Console.ReadLine().Trim();
            }

            int[] arr = new int[k];

            CalcCombo(arr, 0, 0); 
        }

        private static void CalcCombo(int[] arr, int index, int prevNum)
        {
            if (index == arr.Length)
            {
                PrintCombo(arr);
            } 
            else
            {
                for (int i = prevNum; i < stringSet.Count(); i++)
			    {
                    arr[index] = i;
                    CalcCombo(arr, index + 1, i + 1);
			    }           
            }
        }

        private static void PrintCombo(int[] arr)
        {
            string output = null;
            foreach(int i in arr)
            {
                output += stringSet[i] + " "; 
            }
            Console.WriteLine("( " + output + ")");
            countOfPerm++;
        }
    }
}
