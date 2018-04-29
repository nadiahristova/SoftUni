namespace CompanyHierarchy.Interfaces
{
    using Entities.Sales;

    using System.Collections.Generic;

    interface ISalesEmployee
    {
        IEnumerable<Sale> Sales { get; set; }
    }
}
