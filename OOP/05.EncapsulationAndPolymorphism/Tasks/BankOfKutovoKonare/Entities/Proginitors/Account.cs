namespace BankOfKutovoKonare.Entities.Proginitors
{
    using System;
    using Interfaces;

    abstract class Account : IAccount
    {
        private decimal balance;
        private double interestRate;
        private DateTime creatonDate;

        private readonly ICustomer customer;        

        private static readonly DateTime minCreatonDate = new DateTime(1985, 1, 1, 0, 0, 0);

        protected Account(double interestRate, decimal balance, ICustomer customer, DateTime dateCreated)
        {
            this.InterestRate = interestRate;
            this.Balance = balance;
            this.CreatonDate = dateCreated;

            this.customer = customer;
        }

        public decimal Balance
        {
            get { return this.balance; }
            set { this.balance = value; }
        }

        public virtual ICustomer Customer { get { return this.customer; } }

        public double InterestRate
        {
            get { return this.interestRate; }
            set
            {
                if (value < -100 || value > 100)
                    throw new ArgumentOutOfRangeException("interestRate", "Out of range [-100, 100].");
            }
        }

        public DateTime CreatonDate
        {
            get { return this.creatonDate; }
            private set
            {
                if (value.CompareTo(minCreatonDate) < 0 || value.CompareTo(DateTime.Now) > 0)
                    throw new ArgumentOutOfRangeException($"Accounts with creaton date earlier than {minCreatonDate.ToString("dd.MM, yyyy")} are considured out of date.");

                this.creatonDate = value;
            }
        }

        public void DepositMoney(decimal sum)
        {
            this.Balance += sum;
        }

        public virtual double CalculateInterestForPeriod(int months)
        {
            return (double)this.Balance * (1 + this.InterestRate * months);
        }
    }
}
