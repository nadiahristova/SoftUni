namespace BankOfKutovoKonare.Entities.Accounts
{
    using System;
    using Interfaces;
    using Proginitors;
    using Customers;

    class MortgageAccount : Account
    {
        public MortgageAccount(double interestRate, decimal balance, ICustomer customer, DateTime dateCreated) 
            : base(interestRate, balance, customer, dateCreated)
        {
        }

        /// <summary>
        /// Number of mounths indivuals are free of interest rate.</summary>
        private const byte UnchargedMountsIndividuals = 6;

        /// <summary>
        /// Number of mounths companies have lower interest rate.</summary>
        private const byte UnchargedMountsCompanies = 12;

        /// <summary>
        /// Decrese interest rate for companies.</summary>
        private const double lowerRateCompanies = 0.5;
        private ICustomer agroCompany;
        private decimal v1;
        private decimal v2;

        public override double CalculateInterestForPeriod(int months)
        {      
            int monthsPassed = DateTime.Now.Month - this.CreatonDate.Month 
                + 12 * (DateTime.Now.Year - this.CreatonDate.Year);
            int remainingMonths = 0;

            if (this.Customer.GetType() == typeof(Individual))
            {
                if (monthsPassed > UnchargedMountsIndividuals)
                    return base.CalculateInterestForPeriod(months);

                remainingMonths = months - (UnchargedMountsIndividuals - monthsPassed);

                if (remainingMonths <= 0)
                    return 0;

                return base.CalculateInterestForPeriod(remainingMonths);
            }

            if (this.Customer.GetType() == typeof(Company))
            {
                if (monthsPassed > UnchargedMountsCompanies)
                    return (double)this.Balance * (1 + this.InterestRate * remainingMonths * lowerRateCompanies);

                remainingMonths = months - (UnchargedMountsCompanies - monthsPassed);

                return (double)this.Balance * (1 + this.InterestRate * remainingMonths * lowerRateCompanies
                + this.InterestRate * (months - remainingMonths));
            }

            return base.CalculateInterestForPeriod(months);
        }
    }
}
