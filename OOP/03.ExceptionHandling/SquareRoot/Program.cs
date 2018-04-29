namespace SquareRoot
{
    using System;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            string input = Console.ReadLine();

            double sqrtResult;

            try
            {
                sqrtResult = Sqrt(input);

                Console.WriteLine("\u221A{0} = {1:f2}\n", input, sqrtResult);
            }
            catch (NullReferenceException ne)
            {
                Console.WriteLine("Invalid number. {0}", ne.Message);
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message);
            }
            catch(OverflowException of)
            {
                Console.WriteLine("Invalid number. {0}", of.Message);
            }
            catch (ArgumentOutOfRangeException aor)
            {
                Console.WriteLine("Provided number cannot be negative. \n{0}", aor.Message);
            }
            finally
            {
                Console.WriteLine("Bye bye.\n");
            }
        }

        private static double Sqrt(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new NullReferenceException("No input given.");

            int num;

            try
            {
                num = int.Parse(input);
            }
            catch (FormatException fe)
            {
                throw new FormatException("Inavlid number. " + fe.Message);
            }
            

            double result = Math.Sqrt(num);

            if (double.IsNaN(result))
                throw new ArgumentOutOfRangeException("input", "This program does not support imaginary numbers (i), only real ones.");

            return result;
        }
    }
}
