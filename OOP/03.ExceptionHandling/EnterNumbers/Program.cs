namespace EnterNumbers
{
    using System;

    using CustomExceptions.EnterNumbers;

    class Program
    {
        private const int NumLoops = 10;

        static void Main()
        {
            NumberReader reader = SetBoundariesToReader();

            Console.WriteLine("\nType in \"{0}\" if you want to exit the program.\n", NumberReader.EndCommand);

            for (int i = 0; i < NumLoops; i++)
            {
                try
                {
                    reader.ReadNumberFromConsole();
                }
                catch (GetMeOutOfHere)
                {
                    Console.WriteLine("\n\nTy for participationg.");
                    Environment.Exit(0);//return;
                }
            }

            var numbers = reader.AllNumbers.ToArray();
            Array.Reverse(numbers);

            Console.WriteLine(string.Join(" < ", numbers) + " < " + reader.EndNum);
        }

        private static NumberReader SetBoundariesToReader()
        {
            var boundaryReader = new NumberReader(int.MinValue, int.MaxValue);
            Console.Write("Define min allowed number: ");
            int min = boundaryReader.ReadNumberFromConsole();
            Console.Write("Max allowed number: ");
            int max = boundaryReader.ReadNumberFromConsole();

            while (min + NumLoops >= max)
            {
                Console.WriteLine("\nMax number should be greater than {0}. Try again.", min + NumLoops);
                Console.Write("\nMax allowed number: ");
                max = boundaryReader.ReadNumberFromConsole();
            }

            return new NumberReader(min, max, NumLoops);
        }
    }
}