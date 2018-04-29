namespace DatabaseSearchQueries
{
    using EF_Homework;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class DatabaseSearchQueries
    {
        public static void Main(string[] args)
        {
            using (var context = new SoftUniEntities())
            {
                var wantedEmployees = context.Employees
                                        .Where(e => e.Projects.Any(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003))
                                        .Select(emp => new
                                            {
                                                emp.FirstName,
                                                emp.LastName,
                                                ManagedBy = emp.Manager.FirstName + " " + emp.Manager.LastName,
                                                Projects = emp.Projects.Select(proj => new
                                                                            {
                                                                                proj.Name,
                                                                                proj.StartDate,
                                                                                proj.EndDate
                                                                            })
                                            }).ToList();

                foreach (var emp in wantedEmployees)
                {
                    Console.WriteLine("{0} {1}", emp.FirstName, emp.LastName);
                }

                Console.WriteLine();
                Console.WriteLine();

                var addresses = context.Addresses.OrderByDescending(a => a.Employees.Count)
                                                   .ThenBy(a => a.Town.Name).Take(10)
                                                   .Select(a => new
                                                        {
                                                            Address = a.AddressText,
                                                            Town = a.Town.Name,
                                                            EmployeeCount = a.Employees.Count
                                                        }).ToList();
                foreach (var address in addresses)
                {
                    Console.WriteLine("{0}, {1} - {2} employees",
                        address.Address,
                        address.Town,
                        address.EmployeeCount);
                }

                var employeeInQuestion = context.Employees.Where(emp => emp.EmployeeID == 147).Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.Projects.Select(pr => new
                    {
                        ProjectName = pr.Name
                    }).OrderBy(p => p.ProjectName)
                }).FirstOrDefault();

                var departments = context.Departments.Where(dep => dep.Employees.Count > 5).OrderBy(d => d.Employees.Count)
                                                     .Select(d => new
                                                        {
                                                            DepartmentName = d.Name,
                                                            Manager = d.Manager.FirstName + " " + d.Manager.LastName,
                                                            Employees = d.Employees.Select(e => new
                                                                {
                                                                    e.FirstName,
                                                                    e.LastName,
                                                                    e.HireDate,
                                                                    e.JobTitle
                                                                })
                                                        }).ToList();

                Console.WriteLine(departments.Count);

                foreach(var dep in departments)
                {
                    Console.WriteLine("--{0} - Manager: {1}, Employees: {2}",
                        dep.DepartmentName,
                        dep.Manager,
                        dep.Employees.Count());
                }
            }
        }
    }
}
