namespace InterestCalculator
{
    using System;

    public delegate decimal InterestCalculation(decimal sumMoney, decimal interest, byte years);

    class InterestCalculator
    {
        private readonly InterestCalculation InterestCalc;

        private decimal sumMoney;
        private decimal interest;
        private byte years;

        public InterestCalculator(decimal sumMoney, decimal interest, byte years, InterestCalculation interestCalc)
        {
            this.SumMoney = sumMoney;
            this.Interest = interest;
            this.Years = years;

            this.InterestCalc = interestCalc;
        }

        public decimal SumMoney
        {
            get { return this.sumMoney; }
            set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("Invested sum cannot be negative.");

                    this.sumMoney = value;
                }
        }

        public decimal Interest
        {
            get { return this.interest; }
            set
                {
                    if (value < -100 || value > 100)
                        throw new ArgumentOutOfRangeException("Interest", "Value must be in range [-100, 100]");

                    this.interest = value;
                }
        }

        public byte Years
        {
            get { return this.years; }
            set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("Years", "Value cannot be negative.");

                    this.years = value;
                }
        }

        public string CalcInterest()
        {
            return InterestCalc(this.SumMoney, this.Interest, this.Years).ToString("0.0000");
        }
    }
}
