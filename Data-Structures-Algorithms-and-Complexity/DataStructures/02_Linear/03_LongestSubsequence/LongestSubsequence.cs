using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LongestSubsequence
{
    class LongestSubsequence
    {
        static void Main(string[] args)
        {
            Console.Write("Write a sequence of numbers, separated by spaces: ");
            string input = Console.ReadLine();
            List<int> listOfNums = new List<int>();

            try
            {
                listOfNums = (new List<string>(Regex.Split(input, @"\s+"))).Select(s => int.Parse(s)).ToList();
            }
            catch (FormatException)
            {
                Console.WriteLine("The input must contain at least 1 number.");
                return;
            }

            List<int> longestSequOfNums = new List<int>() { listOfNums[0] };
            List<int> intermediateList = new List<int>() { listOfNums[0] };

            for (int index = 1; index < listOfNums.Count; index++)
            {
                if (listOfNums[index] == intermediateList[0])
                {
                    intermediateList.Add(listOfNums[index]);
                }
                else intermediateList = new List<int>() { listOfNums[index] };

                if (intermediateList.Count > longestSequOfNums.Count)
                    longestSequOfNums = intermediateList;
            }

            Console.WriteLine(string.Join( " ", longestSequOfNums));
        }
    }
}
