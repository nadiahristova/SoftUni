using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class FindWordsInFile
{
    private static Dictionary<string, int> setOfWords = new Dictionary<string, int>();
    static void Main()
    {
        int inputCounts = int.Parse(Console.ReadLine().Trim());

        for (int i = 0; i < inputCounts; i++ )
        {
            string line = Console.ReadLine();
            var words = Regex.Matches(line, @"[\w+\#]+").Cast<Match>()
                            .Select(m => m.Value).ToArray();

            foreach (var word in words)
            {
                if (!setOfWords.ContainsKey(word))
                    setOfWords[word] = 0;
                setOfWords[word]++;
            }
        }

        int searchWordCount = int.Parse(Console.ReadLine().Trim());

        for (int i = 0; i < inputCounts; i++)
        {
            Console.WriteLine();
            string word = Console.ReadLine();
            if(setOfWords.ContainsKey(word.Trim()))
                Console.WriteLine("{0} -> {1}", word, setOfWords[word]);
            else Console.WriteLine("{0} -> 0", word);
        }
    }
}

