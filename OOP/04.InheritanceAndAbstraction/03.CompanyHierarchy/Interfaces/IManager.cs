namespace CompanyHierarchy.Interfaces
{
    using Entities.Parents;

    using System.Collections.Generic;

    interface IManager : IEmployee
    {
        ICollection<Employee> ManagedEmployees { get; set; }
    }
}
