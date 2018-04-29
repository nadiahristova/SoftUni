using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using CustomDictionary;

namespace P2.CountSymbols
{
    public class CountSymbols
    {
        public static void Main()
        {
            //string input = "SoftUni rocks";
            string input = "Did you know Math.Round rounds to the nearest even integer?";
            var charList = input.ToCharArray();
            var groups = charList.GroupBy(s => s).Select(s => new
                {
                    Letter = (char)s.Key,
                    Occurrences = s.Count()
                }).ToList();
            CustomDictionary<char, int> infoHolder = new CustomDictionary<char, int>();
            groups.ForEach(g => infoHolder.Add(g.Letter, g.Occurrences));
            PrintLetterOccurrences(infoHolder);
        }

        private static void PrintLetterOccurrences(CustomDictionary<char, int> infoHolder)
        {
            infoHolder.OrderBy(ch => ch.Key).ToList().ForEach(i => Console.WriteLine("{0} : {1} time/s", i.Key, i.Value));
        }
    }
}
