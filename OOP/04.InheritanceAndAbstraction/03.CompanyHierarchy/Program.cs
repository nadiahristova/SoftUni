namespace CompanyHierarchy
{
    using Entities.Sales;
    using Entities.Persons;
    using Entities.Parents;
    using Entities.Projects;
    using Entities.Parents.Progenitor;  

    using System;
    using System.Linq;
    using System.Collections.Generic;    

    class Program
    {
        private static readonly string[] firstNames = { "Kiril", "Didi", "Kiki", "Slavina", "Iliq" };
        private static readonly string[] lastNames = { "Stamenov/a", "Grigorov/a", "Tutev/a", "Simeonov/a" };

        static void Main()
        {
            var rnd = new Random();
            int counter = 4;

            var sales = new HashSet<Sale>();
            var projects = new HashSet<Project>();

            var employees = new HashSet<Employee>();
            var salesEmployees = new HashSet<Employee>();

            var customers = new HashSet<Customer>();

            for (int i = 0; i < 4; i++)
            {
                var sale = new Sale("Product" + i, rnd.Next(0, 200000));
                sales.Add(sale);
            }

            for (int i = 0; i < 4; i++)
            {
                var project = new Project("Project" + i, DateTime.Now);
                projects.Add(project);
            }

            do
            {
                var customer = new Customer(rnd.Next(1, 99),
                                firstNames[rnd.Next(0, firstNames.Length)],
                                lastNames[rnd.Next(0, lastNames.Length)],
                                rnd.Next(0, 200000)
                            );

                customers.Add(customer);
            } while (counter > customers.Count());

            counter = 8;

            do
            {
                var seller = new SalesEmployee(rnd.Next(3, 99),
                                firstNames[rnd.Next(0, firstNames.Length)],
                                lastNames[rnd.Next(0, lastNames.Length)],
                                sales.ToList().GetRange(0, sales.Count)
                            );

                salesEmployees.Add(seller);
            } while (counter > salesEmployees.Count());

            var developer = new Developer(3, "Peter", "Zlatanov", projects);

            var manager1 = new Manager(2, "Iliq", "Petrov");
            var manager2 = new Manager(1, "Mariq", "Petkova", developer, manager1);
            manager2.AddManagedEmployees(salesEmployees);
            
            employees.Add(manager1);
            employees.Add(manager2);
            employees.Add(developer);
            employees.UnionWith(salesEmployees);

            //Console.WriteLine("All employees: \n");
            //employees.ToList().ForEach(Console.WriteLine);

            var persons = new List<Person>();
            persons.AddRange(employees);
            persons.AddRange(customers);

            Console.WriteLine("All persons: \n");
            persons.ForEach(Console.WriteLine);
        }
    }
}
