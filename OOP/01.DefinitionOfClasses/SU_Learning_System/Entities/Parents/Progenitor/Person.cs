namespace SU_Learning_System.Entities.Parents.Progenitor
{    
    using System;

    using SU_Learning_System.Interfaces;

    public abstract class Person : IPerson
    {
        private int age;

        private string firstName;
        private string lastName;

        public static int NumPersons = 0;

        public string FirstName
        {
            get { return this.firstName; }
            set 
                {
                    if (string.IsNullOrEmpty(value))
                        throw new ArgumentException("First Name reuired.");

                    this.firstName = value;
                }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Last Name reuired.");

                this.lastName = value;
            }
        }

        public int Age
        {
            get { return this.age; }
            set
                {
                    if (value < 1 || value > 100)
                        throw new ArgumentOutOfRangeException("Invalid data for age. The value must be within the boundaries [1, 100]");

                    this.age = value;
                }
        }

        protected Person():this("[unknown first name]", "[unknown last name]", 0) { }

        protected Person(string firstName, string lastName, int age)
        {
            this.Age = age;
            this.FirstName = firstName;
            this.LastName = lastName;

            Person.NumPersons++;
        }
    }
}
