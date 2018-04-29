using CustomDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P3.Phonebook
{
    public class Phonebook
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            CustomDictionary<string, string> infoHolder = new CustomDictionary<string, string>();

            bool searchRegime = false;
            do
            {
                if(input.ToLower().Trim() == "search")
                    searchRegime ^= true;

                if(!searchRegime)
                {
                    string[] info = Regex.Split(input, @"\s*\-\s*");
                    infoHolder.AddOrReplace(info[0], info[1]);
                }
                else
                {
                    if (input.ToLower().Trim() == "search") 
                        input = Console.ReadLine();
                    string phoneNumber;
                    if(infoHolder.TryGetValue(input.Trim(), out phoneNumber))
                        Console.WriteLine("{0} -> {1}", input.Trim(), phoneNumber);
                    else
                        Console.WriteLine("Contact {0} does not exist.", input);
                }

                input = Console.ReadLine();
            } while(!string.IsNullOrEmpty(input));
        }
    }
}
