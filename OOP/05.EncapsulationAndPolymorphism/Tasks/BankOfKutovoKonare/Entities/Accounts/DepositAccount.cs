namespace BankOfKutovoKonare.Entities.Accounts
{
    using System;
    using Interfaces;
    using Proginitors;

    class DepositAccount : Account, IWithdraw
    {
        public DepositAccount(double interestRate, decimal balance, ICustomer customer, DateTime dateCreated) 
            : base(interestRate, balance, customer, dateCreated)
        {
        }

        public bool WithdrawMoney(decimal sum)
        {
            if (this.Balance < sum)
                return false;

            this.Balance -= sum;
            return true;
        }

        public override double CalculateInterestForPeriod(int months)
        {
            if (this.Balance > 0 && this.Balance < 1000)
                return 0;

            return base.CalculateInterestForPeriod(months);            
        }
    }
}
