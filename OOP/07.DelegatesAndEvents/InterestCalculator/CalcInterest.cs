namespace InterestCalculator
{
    using System;

    class CalcInterest
    {
        private const int n = 12;

        static void Main()
        {
            //InterestCalculation overallSimpleInterest = (sumMoney, interest, years)
            //    =>
            //        {
            //            return Math.Round(sumMoney *(1 + years * interest / 100), 4);
            //        };

            InterestCalculation overallSimpleInterest = GetSimpleInterest;
            var simpleInterest = new InterestCalculator(2500, 7.2m, 15, overallSimpleInterest);
            Console.WriteLine("Simple interest sum: " + simpleInterest.CalcInterest());

            InterestCalculation overallCompoundInterest = GetCompoundInterest;
            var compoundInterest = new InterestCalculator(500, 5.6m, 10, overallCompoundInterest);
            Console.WriteLine("Compound interest sum: " + compoundInterest.CalcInterest());
        }

        public static decimal GetSimpleInterest(decimal sumMoney, decimal interest, byte years)
        {
            return sumMoney * (1 + years * interest / 100);
        }

        public static decimal GetCompoundInterest(decimal sumMoney, decimal interest, byte years)
        {
            return sumMoney * (decimal)Math.Pow((double)(1 + interest / 100 / n), years * n);
        }
    }
}
