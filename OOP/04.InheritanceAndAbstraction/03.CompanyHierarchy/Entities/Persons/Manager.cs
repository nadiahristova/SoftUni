namespace CompanyHierarchy.Entities.Persons
{
    using Parents;
    using Interfaces;

    using System.Text;
    using System.Linq;
    using System.Collections.Generic;    

    class Manager : Employee, IManager
    {
        public Manager(int id, string firstName, string lastName, ICollection<Employee> managedEmployees) 
            : base(id, firstName, lastName)
        {
            this.ManagedEmployees = managedEmployees;
        }

        public Manager(int id, string firstName, string lastName, params Employee[] managedEmployees) 
            : base(id, firstName, lastName)
        {
            this.ManagedEmployees = managedEmployees.ToList();
        }

        public Manager(int id, string firstName, string lastName) : this(id, firstName, lastName, new HashSet<Employee>())
        { }

        public ICollection<Employee> ManagedEmployees { get; set; }

        public void AddManagedEmployees(ICollection<Employee> employees)
        {
            foreach (var emp in employees)
            {
                this.ManagedEmployees.Add(emp);
            }
        }

        public override string ToString()
        {
            var managerInfo = new StringBuilder();
            managerInfo.AppendLine("Manager: " + base.ToString());
            managerInfo.AppendLine("Managed employees:");

            if(!this.ManagedEmployees.Any())
                managerInfo.AppendLine("None at the moment.");
            else
                foreach(var emp in this.ManagedEmployees)
                    managerInfo.AppendLine($"{emp.GetType().Name}: {emp.FirstName} {emp.LastName}, ID: {emp.Id}");   
                     
            return managerInfo.ToString();
        }
    }
}
