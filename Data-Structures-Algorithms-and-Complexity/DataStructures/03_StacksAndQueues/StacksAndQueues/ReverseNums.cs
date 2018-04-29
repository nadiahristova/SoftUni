using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StacksAndQueues
{
    class ReverseNums
    {
        static void Main(string[] args)
        {
            Stack<int> helper = new Stack<int>();
            Console.Write("Write a sequence of integer numbers separated by space(s):");
            string input = Console.ReadLine();
            int[] nums = new int[0];

            try
            {
                if (input != "" && input != null)
                    nums = Regex.Split(input, @"\s+").Select(x => int.Parse(x)).ToArray();
                foreach (int num in nums)
                {
                    helper.Push(num);
                }

                Console.WriteLine("Reversed sequence is: " + String.Join(" ", helper.ToArray()));
            }
            catch (FormatException)
            {
                Console.WriteLine("<< Invalid entry >>\nThe input data should be made up by integer numbers separated by empty spaces\nPlease try again.");
            } 
        }
    }
}
