namespace CompanyHierarchy.Entities.Parents
{
    abstract class RegularEmployee : Employee
    {
        protected RegularEmployee(int id, string firstName, string lastName) : base(id, firstName, lastName)
        {  }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
