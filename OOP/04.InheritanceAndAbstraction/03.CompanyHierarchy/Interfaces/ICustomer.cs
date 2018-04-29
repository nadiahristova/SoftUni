using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHierarchy.Interfaces
{
    interface ICustomer : IPerson
    {
        decimal MoneySpent { get; set; }
    }
}
