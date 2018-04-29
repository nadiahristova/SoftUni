namespace Workers.Entities.Progenitor
{
    using System;
    using System.Text.RegularExpressions;

    abstract class Human
    {
        private const int NameMinLength = 4;
        private static readonly Regex nameValidationRegex = new Regex("^([a-zA-Z/]+)$");

        private string firstName;

        private string lastName;

        protected Human(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
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
        {
            return string.Format("{0} {1}", this.FirstName, this.LastName);
        }
    }
}
