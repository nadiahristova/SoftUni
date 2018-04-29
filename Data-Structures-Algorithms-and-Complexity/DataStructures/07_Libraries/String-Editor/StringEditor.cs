using System;
using System.Collections.Generic;
using System.Linq;

using Wintellect.PowerCollections;

namespace String_Editor
{
    class StringEditor
    {
        private static BigList<char> content = new BigList<char>();
        private static bool wrongCommandSyntax;
        static void Main()
        {
            string line = null;

            do
            {
                if (!wrongCommandSyntax)
                {
                    line = Console.ReadLine();
                    string[] data = line.Split(new[] { ' ' }, 2);
                    ProcessData(data);
                }
                else
                {
                    Console.WriteLine("Unknown Command. Please try again or press Enter to exit the program.");
                    wrongCommandSyntax = false;
                }
            } while (!string.IsNullOrEmpty(line));
        }

        private static void ProcessData(string[] data)
        {
            switch (data[0].Trim().ToLower())
            {
                case "insert":
                    {
                        string someString = data[1];
                        //content.InsertRange(0, someString.ToCharArray());
                        content.AddRangeToFront(someString.ToCharArray());
                        Console.WriteLine("Roger that");
                    } break;
                case "append":
                    {
                        string someString = data[1];
                        content.AddRange(someString.ToCharArray());
                        //someString.ToCharArray().ToList().ForEach(e => content.Add(e));
                        Console.WriteLine("Roger that");
                    } break;
                case "delete":
                    {
                        var intData = data[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                        if (intData.Count() == 2)
                        {
                            if (intData[0] + intData[1] < content.Count)
                            {
                                content.RemoveRange(intData[0], intData[1]);
                                Console.WriteLine("Roger that");
                            }
                            else
                            {
                                Console.WriteLine("ERROR. Please try again or press Enter to exit the program.");
                            }
                        }
                        else wrongCommandSyntax = true;
                    } break;
                case "replace":
                    {
                        var strData = data[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        if (strData.Count() == 3)
                        {
                            int index = int.Parse(strData[0]);
                            int length = int.Parse(strData[1]);

                            if (index + length < content.Count)
                            {
                                content.RemoveRange(index, length);
                                content.InsertRange(index, strData[2].ToCharArray());
                                Console.WriteLine("Roger that");
                            }
                            else
                            {
                                Console.WriteLine("ERROR. Please try again or press Enter to exit the program.");
                            }
                        }
                        else wrongCommandSyntax = true;
                    } break;
                case "print":
                    {
                        content.ForEach(item => Console.Write(item));
                        Console.WriteLine();
                    } break;
                default: wrongCommandSyntax = true; break;
            }
        }
    }
}
