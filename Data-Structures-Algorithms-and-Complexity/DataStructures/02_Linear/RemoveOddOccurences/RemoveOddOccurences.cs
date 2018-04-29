using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RemoveOddOccurences
{
    class RemoveOddOccurences
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

            List<int> outputListOfNums = listOfNums.ToList();

            foreach (int num in listOfNums)
            {
                int count = listOfNums.Where(x => x.Equals(num)).Count();

                if (count % 2 != 0)
                {
                    outputListOfNums.RemoveAll(x => x == num);
                }
            }

            Console.WriteLine(string.Join(" ", outputListOfNums));
        }
    }
}
