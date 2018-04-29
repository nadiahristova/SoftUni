using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SortWords
{
    class SortWords
    {
        static void Main(string[] args)
        {
            Console.Write("Write a sequence of words, separated by spaces: ");
            string input = Console.ReadLine();
            List<string> listOfWords = (new List<string>(Regex.Split(input, @"\s+")));
            listOfWords.Sort();

            string output = string.Join( " ", listOfWords);
            Console.WriteLine(output);
        }
    }
}
