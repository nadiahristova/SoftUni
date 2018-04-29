namespace FractionCalculator.Structs
{
    using System;

    struct Fraction
    {
        private int denumerator;

        public Fraction(int numerator, int denumerator) : this()
        {
            this.Numerator = numerator;
            this.Denomerator = denumerator;
        }

        public int Numerator { get; set; }

        public int Denomerator
        {
            get { return this.denumerator; }
            set
            {
                if (value == 0)
                    throw new ArgumentException(nameof(Denomerator), new DivideByZeroException($" cannot be zero."));

                this.denumerator = value;
            }
        }

        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            int sumNumerator;
            int sumDenumerator;

            try
            {
                sumNumerator = checked(f1.Numerator * f2.Denomerator + f1.Denomerator * f2.Numerator);
                sumDenumerator = checked(f1.Denomerator * f2.Denomerator);
            }
            catch (OverflowException ofe)
            {
                throw new ArgumentException("Add fraction", ofe);
            }

            return new Fraction(sumNumerator, sumDenumerator);
        }

        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            int divNumerator;
            int divDenumerator;

            try
            {
                divNumerator = checked(f1.Numerator * f2.Denomerator - f1.Denomerator * f2.Numerator);
                divDenumerator = checked(f1.Denomerator * f2.Denomerator);
            }
            catch (OverflowException ofe)
            {
                throw new ArgumentException("Subtract fraction", ofe);
            }

            return new Fraction(divNumerator, divDenumerator);
        }

        public override string ToString()
            => $"{this.Numerator / (decimal)this.Denomerator}";
    }
}
