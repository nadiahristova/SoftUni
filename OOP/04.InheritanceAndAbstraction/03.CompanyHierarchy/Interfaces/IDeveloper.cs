namespace CompanyHierarchy.Interfaces
{
    using Entities.Projects;

    using System.Collections.Generic;

    interface IDeveloper
    {
        IEnumerable<Project> Projects { get; set; }
    }
}
