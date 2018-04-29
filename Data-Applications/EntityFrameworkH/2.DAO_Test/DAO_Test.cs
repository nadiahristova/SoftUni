namespace DAO_Test
{
    using EF_Homework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class DAO_Test
    {
        static void Main(string[] args)
        {
            Employee newEmployee = new Employee
            {
                FirstName = "Vasko777",
                LastName = "Ivanov",
                JobTitle = "Sales Manager",
                DepartmentID = 2,
                HireDate = DateTime.Now,
                Salary = 10000
            };

            DAOEmployees.Add(newEmployee);

            var primaryKey = newEmployee.EmployeeID;
            Console.WriteLine("{0}'s ID Key is {1}", newEmployee.FirstName, primaryKey);

            DAOEmployees.Modify(newEmployee, "Iliq");

            DAOEmployees.Delete(newEmployee); 
        }
    }
}
