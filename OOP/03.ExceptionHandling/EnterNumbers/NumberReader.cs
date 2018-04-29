namespace EnterNumbers
{
    using System;
    using System.Collections.Generic;

    using CustomExceptions.EnterNumbers;    

    class NumberReader
    {
        public const string EndCommand = "end";

        private int numAccepted = 0;

        public readonly int StartNum;
        public readonly int EndNum;
        private readonly int? span;

        public Stack<int> AllNumbers { get; private set; }

        public NumberReader(int start, int end, int? span = null)
        {
            StartNum = start;
            EndNum = end;
            this.span = span;
            
            this.AllNumbers = new Stack<int>();
            this.AllNumbers.Push(start);
        }

        public int ReadNumberFromConsole()
        {
            int? num = null;
            string line = null;

            try
            {
                line = ReadLineFromConsole();
                num = int.Parse(line);
            }
            catch (NullReferenceException nfe)
            {
                Console.Error.WriteLine(nfe.Message);
                this.ReadNumberFromConsole();               
            }
            catch (FormatException)
            {
                Console.Error.WriteLine("\"{0}\" is invalid integer.", line);
                this.ReadNumberFromConsole();
            }
            catch (OverflowException of)
            {
                Console.Error.WriteLine(of.Message);
                this.ReadNumberFromConsole();
            }

            try
            {
                ValidateNumber(num.Value);
            }
            catch (ArgumentException arge)
            {
                Console.Error.WriteLine(arge.Message);
                this.ReadNumberFromConsole();
            }
            catch (NullReferenceException)
            {
                Console.Error.WriteLine("Value cannot be null. Try again.");
                this.ReadNumberFromConsole();
            }

            return num.Value;
        }

        private void ValidateNumber(int value)
        {
            int prevNum = this.AllNumbers.Peek();

            if (value <= StartNum || value >= EndNum)
                throw new ArgumentException("The number was out of the given range (" + StartNum + "; " + EndNum + ").");

            if (value <= prevNum)
                throw new ArgumentException("The number provided is smaller or equal to the precedent number. " + value + " <= " + prevNum);
                        
            if (span != null && (this.EndNum - (this.span.Value - this.numAccepted - 1) <= value))
                throw new ArgumentException("You have " + (span.Value - this.numAccepted) + " numbers left to enter. In this rate the task is impossible. Your next number should be no greater than " + 
                    (EndNum - (span.Value - this.numAccepted)) + "." );

            this.numAccepted++;
            this.AllNumbers.Push(value);
        }

        private static string ReadLineFromConsole()
        {
            string line = Console.ReadLine().Trim();

            if (line.ToLower() == EndCommand)
                throw new GetMeOutOfHere();

            if (string.IsNullOrEmpty(line))
                throw new NullReferenceException("Value cannot be null. Try again.");

            return line;
        }
    }
}
