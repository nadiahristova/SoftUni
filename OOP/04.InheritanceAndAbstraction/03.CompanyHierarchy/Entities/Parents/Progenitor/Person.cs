namespace CompanyHierarchy.Entities.Parents.Progenitor
{
    using System;    
    using System.Text.RegularExpressions;

    using Interfaces;

    abstract class Person : IPerson
    {
        private int id;
        private string firstName;
        private string lastName;

        private const int NameMinLength = 4;
        private static readonly Regex nameValidationRegex = new Regex("^([a-zA-Z/]+)$");      

        protected Person(int id, string firstName, string lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public int Id
        {
            get { return this.id; }
            set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("Id cannot be negative number.");

                    this.id = value;
                }
        }

        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("First name is required.");

                value = value.Trim();

                if (value.Length < NameMinLength || !nameValidationRegex.IsMatch(value))
                    throw new ArgumentException("First name should be no less than " + NameMinLength
                        + " letters long and must contain only latin letters.");

                this.firstName = value;
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Last name is required.");

                value = value.Trim();

                if (value.Length < NameMinLength || !nameValidationRegex.IsMatch(value))
                    throw new ArgumentException("Last name should be no less than " + NameMinLength
                        + " letters long and must contain only latin letters.");

                this.lastName = value;
            }
        }

        public override string ToString() 
            => $"{this.FirstName} {this.LastName}, ID: {this.Id}";
    }
}
