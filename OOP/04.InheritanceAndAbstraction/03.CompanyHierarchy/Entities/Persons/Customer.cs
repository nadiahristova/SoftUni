namespace CompanyHierarchy.Entities.Persons
{
    using System;

    using Interfaces;
    using Parents.Progenitor;

    class Customer : Person, ICustomer, IEquatable<Customer>
    {
        private decimal moneySpent;
        
        public Customer(int id, string firstName, string lastName, decimal moneySpent = 0m) : base(id, firstName, lastName)
        {
            this.MoneySpent = moneySpent;
        }

        public decimal MoneySpent
        {
            get { return this.moneySpent; }
            set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("Purchase money cannot be a negative number.");

                    this.moneySpent = value;
                }
        }

        public bool Equals(Customer other)// we need this because we use HashSet
        {
            return this.Id == other.Id;
        }

        public override string ToString()
            => $"Customer: {base.ToString()}\nMoney Spent: {this.MoneySpent}";
    }
}
