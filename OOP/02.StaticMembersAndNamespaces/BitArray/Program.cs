namespace BitArray
{
    using System;

    class Program
    {
        static void Main()
        {
            var number = new BitArray(25);
            number[2] = 1;
            Console.WriteLine(number.Number);
            Console.WriteLine(number.BitsSnapShot(false));

            number[2] = 0;
            Console.WriteLine(number.ToString());
            Console.WriteLine(number.BitsSnapShot(false));

            number[7] = 1;
            Console.WriteLine(number.ToString());
            Console.WriteLine(number.BitsSnapShot());
        }
    }
}
