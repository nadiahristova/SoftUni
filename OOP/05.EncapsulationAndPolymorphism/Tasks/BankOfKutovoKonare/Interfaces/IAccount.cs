namespace BankOfKutovoKonare.Interfaces
{
    using System;

    interface IAccount : IDeposit
    {
        DateTime CreatonDate { get; }

        ICustomer Customer { get; }

        double InterestRate { get; set; }

        decimal Balance { get; set; }

        double CalculateInterestForPeriod(int months);
    }
}
