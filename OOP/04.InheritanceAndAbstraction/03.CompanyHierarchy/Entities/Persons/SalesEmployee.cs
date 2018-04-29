namespace CompanyHierarchy.Entities.Persons
{   
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
        
    using Sales;
    using Parents;
    using Interfaces;

    class SalesEmployee : RegularEmployee, ISalesEmployee
    {
        public SalesEmployee(int id, string firstName, string lastName, IEnumerable<Sale> sales) 
            : base(id, firstName, lastName)
        {
            this.Sales = sales;
        }

        public SalesEmployee(int id, string firstName, string lastName, params Sale[] sales) 
            : base(id, firstName, lastName)
        {
            this.Sales = sales;
        }

        public SalesEmployee(int id, string firstName, string lastName) : this(id, firstName, lastName, new HashSet<Sale>())
        { }

        public IEnumerable<Sale> Sales { get; set; }

        public override string ToString()
        {
            var managerInfo = new StringBuilder();
            managerInfo.AppendLine($"Seller: {base.ToString()}");
            managerInfo.AppendLine("Sales:");

            if (!this.Sales.Any())
                managerInfo.AppendLine("None so far.");
            else
                foreach (var sale in this.Sales)
                    managerInfo.AppendLine(sale.ToString());

            return managerInfo.ToString();
        }
    }
}
