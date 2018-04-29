namespace NativeSQLQuery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EF_Homework;
    using System.Diagnostics;
    class NativeSQLQuery
    {
        static void Main(string[] args)
        {
            var context = new SoftUniEntities();

            var totalCount = context.Employees.Count();

            var sw = new Stopwatch();
            sw.Start();

            PrintNamesWithNativeQuery();
            Console.WriteLine("\nNative: {0}\n\n", sw.Elapsed);

            sw.Restart();

            PrintNamesWithLinqQuery_LINQtoEntities();

            Console.WriteLine("\n\nLINQtoEntities: {0}\n\n", sw.Elapsed);

            sw.Restart();

            PrintNamesWithLinqQuery_WithExtensionMethods();

            Console.WriteLine("\n\nLINQWithExtensionMethods: {0}", sw.Elapsed);

            sw.Stop();
        }

        private static void PrintNamesWithLinqQuery_WithExtensionMethods()
        {
            using (var ctx = new SoftUniEntities())
            {
                var queryResult = ctx.Employees
                    .Where(emp => emp.Projects.Any(p => p.StartDate.Year == 2002))
                    .Select(emp => emp.FirstName);

                var employees = queryResult.ToList();

                Console.WriteLine(string.Join(" | ", employees));
            }
        }

        private static void PrintNamesWithLinqQuery_LINQtoEntities()
        {
           using(var ctx = new SoftUniEntities())
           {
               var queryResult = (from e in ctx.Employees
                                 where (from p in e.Projects
                                            where p.StartDate.Year == 2002
                                            select p).Any()
                                 select e.FirstName);
               //https://smehrozalam.wordpress.com/2010/06/29/entity-framework-queries-involving-many-to-many-relationship-tables/
               
               Console.WriteLine(string.Join(" | ", queryResult));
            }
        }

        private static void PrintNamesWithNativeQuery()
        {
           using(var ctx = new SoftUniEntities())
           {
               string nativeQuery =
                    "SELECT e.FirstName FROM Employees e " +
                    "WHERE EXISTS(SELECT p.ProjectID FROM Projects p " + 
                    "LEFT JOIN EmployeesProjects ep " +
                    "ON p.ProjectID = ep.ProjectID " +
                    "LEFT JOIN Employees em " +
                    "ON ep.EmployeeID = em.EmployeeID " +
                    "WHERE em.EmployeeID = e.EmployeeID " + 
                    "AND YEAR(p.StartDate) = 2002)";

               var queryResult = ctx.Database.SqlQuery<string>(nativeQuery);
               List<string> employees = queryResult.ToList();

               Console.WriteLine(string.Join(" | ", employees));
           }

        }
    }
}
