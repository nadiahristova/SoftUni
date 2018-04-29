namespace FractionCalculator
{
    using Structs;

    using System;

    class PlayWithCalculator
    {
        static void Main()
        {
            try
            {
                Fraction fraction1 = new Fraction(22, 7);
                Fraction fraction2 = new Fraction(40, 4);
                Fraction result = fraction1 + fraction2;
                Console.WriteLine(result.Numerator);
                Console.WriteLine(result.Denomerator);
                Console.WriteLine(result);
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine($"{ae.Message} {ae.InnerException.Message.ToLower()}");
            }
        }
    }
}
