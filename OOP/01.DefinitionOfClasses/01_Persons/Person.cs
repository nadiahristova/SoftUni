namespace Person
{
    using System;
    using System.Linq;
    using System.Net.Mail;

    class Person
    {
        private int age;
        private string name;        
        private string email;

        public string Name
        {
            get { return this.name; }
            set
                {
                    if(string.IsNullOrEmpty(value))
                        throw new ArgumentException("The name cannot be empty");

                    this.name = value;
                }
        }

        public int Age
        {
            get { return this.age; }
            set
                {      
                    if (value < 1 || value > 100)
                        throw new ArgumentOutOfRangeException("Invalid data for age. The value must be in the boundaries [1, 100]");

                    this.age = value;
                }
        }      

        public string Email
        {
            get { return this.email; }
            set
                {
                    if(value != null)
                        try
                        {
                            MailAddress m = new MailAddress(value);
                        }
                        catch (FormatException)
                        {
                            throw new FormatException("Email provided is not in the valid format.");
                        }

                    this.email = value;
                }
        }

        public Person(string name, int age, string email = null)
        {
            this.Name = name;
            this.Age = age;
            this.Email = email;
        }   

        public override string ToString()
        {   
            if(this.email == null)
                return String.Format("{0} is {1} years old.", this.name, this.age);
            else
                return String.Format("{0} is {1} years old. \nYou can contact him/her at {2}.", this.name, this.age, this.email);
        }

        public static void Main()
        {
            Person p1 = new Person("Ivan", 18, "vankata@abv.bg");
            Person p2 = new Person("Ioana", 27);
            //Person p3 = new Person("Ivancho", 18, "Ivanchoabv.bg");
            Person[] persons = { p1, p2 };

            persons.ToList().ForEach(Console.WriteLine);
        }
    }
}
