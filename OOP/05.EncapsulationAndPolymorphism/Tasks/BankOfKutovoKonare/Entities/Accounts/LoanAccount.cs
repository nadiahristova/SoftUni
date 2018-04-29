namespace BankOfKutovoKonare.Entities.Accounts
{
    using System;
    using Customers;
    using Interfaces;
    using Proginitors;

    class LoanAccount : Account
    {
        /// <summary>
        /// Number of mounths indivuals are free of interest rate.</summary>
        private const byte UnchargedMountsIndividuals = 3;

        /// <summary>
        /// Number of mounths companies are free of interest rate.</summary>
        private const byte UnchargedMountsCompanies = 2;

        public LoanAccount(double interestRate, decimal balance, ICustomer customer, DateTime dateCreated) 
            : base(interestRate, balance, customer, dateCreated)
        {
        }

        public override double CalculateInterestForPeriod(int months)
        {
            if (this.Customer.GetType() == typeof(Individual) || this.Customer.GetType() == typeof(Company))
            {
                int monthsPassed = DateTime.Now.Month - this.CreatonDate.Month + 12 * (DateTime.Now.Year - this.CreatonDate.Year);
                int remainingMonths = 0;

                if (this.Customer.GetType() == typeof(Individual))
                {
                    if (monthsPassed > UnchargedMountsIndividuals)
                        return base.CalculateInterestForPeriod(months);

                    remainingMonths = months - (UnchargedMountsIndividuals - monthsPassed);                    
                }
                else
                {
                    if (monthsPassed > UnchargedMountsCompanies)
                        return base.CalculateInterestForPeriod(months);

                    remainingMonths = months - (UnchargedMountsCompanies - monthsPassed);                    
                }

                if (remainingMonths <= 0)
                    return 0;

                return base.CalculateInterestForPeriod(remainingMonths);
            }

            return base.CalculateInterestForPeriod(months);
        }
    }
}
