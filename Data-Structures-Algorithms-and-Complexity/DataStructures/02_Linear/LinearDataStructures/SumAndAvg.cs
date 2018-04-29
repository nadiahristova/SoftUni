using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SumAndAvg
{
    class SumAndAvg
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
            catch(FormatException)
            {
                Console.WriteLine("Sum=0; Average=0");
                return;
            }

            int sum = listOfNums.Sum();
            double avg = listOfNums.Average();

            Console.WriteLine("Sum = {0}; Average = {1}", sum, avg);
        }
    }
}
