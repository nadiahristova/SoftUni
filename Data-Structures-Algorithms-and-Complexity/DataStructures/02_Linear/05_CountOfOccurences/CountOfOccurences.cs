using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CountOfOccurences
{
    class CountOfOccurences
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

            listOfNums.Sort();
            var result = listOfNums.GroupBy(i => i);

            foreach (var group in result)
            {
                Console.WriteLine("{0} -> {1} time(s)", group.Key, group.Count());
            }
        }
    }
}
