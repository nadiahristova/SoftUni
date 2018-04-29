namespace BankOfKutovoKonare
{
    using Interfaces;
    using Entities.Accounts;

    using System;
    using Entities.Customers;
    using System.Collections.Generic;    

    class BankSystem
    {
        static void Main()
        {
            ICustomer person = new Individual("Valentin Slovakov");
            ICustomer company = new Company("Simpsons Ltd.");

            IAccount mortgageAccInd = new MortgageAccount(1.3, 1204m, person, DateTime.Now);
            IAccount mortgageAccComp = new MortgageAccount(1.3, 1204m, company, DateTime.Now);
            IAccount loanAccInd = new LoanAccount(0.7, 10204m, person, DateTime.Now.AddMonths(-5));
            IAccount loanAccComp = new LoanAccount(0.2, 1054m, company, DateTime.Now);
            IAccount depositAccIndBig = new DepositAccount(2.8, 60604m, person, DateTime.Now.AddMonths(-1));
            IAccount depositAccIndSmall = new DepositAccount(4.2, 544m, person, DateTime.Now.AddMonths(-24));
            IAccount depositAccComp = new DepositAccount(4.2, 10544m, company, DateTime.Now.AddMonths(-5));

            List<IAccount> accounts = new List<IAccount>()
            {
                mortgageAccInd,
                mortgageAccComp,
                loanAccInd,
                loanAccComp,
                depositAccIndBig,
                depositAccIndSmall,
                depositAccComp
            };

            foreach (var acc in accounts)
            {
                Console.WriteLine(
                    "{5} {0,-15}: {1:N2}, {2:N2}, {3:N2}, {4:N2}",
                    acc.GetType().Name,
                    acc.CalculateInterestForPeriod(2),
                    acc.CalculateInterestForPeriod(3),
                    acc.CalculateInterestForPeriod(10),
                    acc.CalculateInterestForPeriod(13),
                    acc.Customer.GetType().Name);
            }
        }
    }
}
