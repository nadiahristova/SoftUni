namespace BankOfKutovoKonare.Entities.Proginitors
{
    using System;
    using Interfaces;

    abstract class Customer : ICustomer
    {
        private string name;

        public Customer(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get { return this.name; }
            set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentNullException("name", "Customer name is required.");

                    this.name = value;
                }
        }
    }
}
