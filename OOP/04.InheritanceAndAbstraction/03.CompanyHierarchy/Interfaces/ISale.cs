namespace CompanyHierarchy.Interfaces
{
    using System;

    interface ISale
    {
        string ProductName { get; set; }

        DateTime Date { get; set; }

        decimal Price { get; set; }
    }
}
