using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SequenceNtoM
{
    class SequenceNtoM
    {
        private class Item
        {
        public int Value{ get; set;}
        public Item PrevItem { get; private set; }

        public Item(int value, Item prevItem = null)
        {
            this.Value = value;
            this.PrevItem = prevItem;
        }

        }
        static void Main(string[] args)
        {
            Queue<Item> numSequenceHolder = new Queue<Item>();
            Console.Write("Type in two integer numbers separated by space(s), respectively the beginning and the end of the sought shortest sequence: ");

            try
            {
                var input = Regex.Split(Console.ReadLine().Trim(), @"\s+").Select(x => int.Parse(x)).ToArray();
                int startNum = input[0];
                int endNum = input[1];
                bool existsSolution = false;

                var currItem = new Item(startNum);
                numSequenceHolder.Enqueue(currItem);

                while (numSequenceHolder.Count != 0)
                {
                    currItem = numSequenceHolder.Dequeue();

                    if (currItem.Value < endNum)
                    {
                        numSequenceHolder.Enqueue(new Item(currItem.Value * 2, currItem));
                        numSequenceHolder.Enqueue(new Item(currItem.Value + 2, currItem));
                        numSequenceHolder.Enqueue(new Item(currItem.Value + 1, currItem));
                    }
                    else if (currItem.Value == endNum)
                    {
                        existsSolution = true;
                        break;
                    }
                }

                if (existsSolution)
                {
                    PrintSolutionSequence(currItem);
                } else Console.WriteLine("(no solution)");
            }
            catch (FormatException)
            {
                Console.WriteLine("<< Invalid entry >>\nPlease try again.");
            }

            
        }

        private static void PrintSolutionSequence(Item item)
        {
            List<int> solutionChain = new List<int>();
            Item currItem = item;

            while (currItem != null)
            {
                solutionChain.Add(currItem.Value);
                currItem = currItem.PrevItem;
            }

            solutionChain.Reverse();

            Console.WriteLine(string.Join(" -> ", solutionChain));
        }
    }
}
