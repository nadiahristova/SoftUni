using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalcSequence
{
    class CalcSequence
    {
        static void Main(string[] args)
        {
            Queue<double> queue = new Queue<double>();
            int index = 0;
            double[] outputNums = new double[50];

            while(true)
            {
                Console.Write("Please enter the first number: ");
                string input = Console.ReadLine().Trim();

                if (Regex.IsMatch(input, @"^[+,-]?\d+.\d+$") || Regex.IsMatch(input, @"^[+,-]?\d+$"))
                {
                    queue.Enqueue(double.Parse(input));
                    break;
                }
                else 
                {
                    Console.CursorLeft = Console.BufferWidth/2 - "<< Invalid input >>".Length/2 -1;
                    Console.WriteLine("<< Invalid input >>\n\nThe first number from the sequence must be a valid number(double/integer).\n");
                } 
            }

            do
            {
                double currEl = queue.Dequeue();
                outputNums[index] = currEl;
                queue.Enqueue(currEl + 1);
                queue.Enqueue(currEl * 2 + 1);
                queue.Enqueue(currEl + 2);
                index++;
            } while (index < 50);

            Console.WriteLine("First 50 numbers form the sequence are: " + string.Join(", ", outputNums));
        }
    }
}
