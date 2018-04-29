namespace CompanyHierarchy.Entities.Parents
{
    using System;

    using Enums;
    using Interfaces;
    using Progenitor;

    abstract class Employee : Person, IEmployee, IEquatable<Employee>
    {
        private decimal salary;

        protected Employee(int id, string firstName, string lastName) : base(id, firstName, lastName)
        { }

        public Department Department { get; set; }

        public decimal Salary
        {
            get { return this.salary; }
            set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("Employee salary cannot be negative.");

                    this.salary = value;
                }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public bool Equals(Employee other)// we need this because we use HashSet
        {
            return this.Id == other.Id;
        }
    }
}
