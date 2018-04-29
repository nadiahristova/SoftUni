namespace CompanyHierarchy.Interfaces
{
    using Enums;

    interface IEmployee : IPerson
    {
        Department Department { get; set; }

        decimal Salary { get; set; }
    }
}
