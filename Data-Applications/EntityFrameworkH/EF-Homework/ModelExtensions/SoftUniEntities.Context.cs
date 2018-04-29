namespace EF_Homework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using System.Data.Linq.Mapping;
    using System.Data.Linq;
    using System.Reflection;
    using System.Linq;

    using System.Data.Entity.Core.Objects;
    using System.Data.SqlClient;

    public partial class SoftUniEntities : DbContext
    {
        public void GetProjectsOfEmployee(string a, string b)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            var EmpFN = new SqlParameter("@EmployeeFirstName", a);
            var EmpLN = new SqlParameter("@EmployeeLastName", b);
            var employeeDetails = this.Database.SqlQuery<clsEmployeeEntity>("GetProjectsOfEmployee @EmployeeFirstName, @EmployeeLastName", EmpFN, EmpLN);

            foreach (var details in employeeDetails)
            {
                Console.WriteLine(details.Name + " - " + details.Description + " , " + String.Format("{0:d/M/yyyy HH:mm:ss}", details.StartDate) + "\n");
            }
        }

            //USE [SoftUni]
            //GO

            //SET ANSI_NULLS ON
            //GO

            //SET QUOTED_IDENTIFIER ON
            //GO


            //CREATE PROCEDURE [dbo].[GetProjectsOfEmployee]
            //      @EmployeeFirstName nvarchar(50),
            //      @EmployeeLastName nvarchar(50)
            //AS
            //BEGIN
            //SET NOCOUNT ON
            //      SELECT p.Name, p.Description, p.StartDate FROM Projects p
            //        JOIN EmployeesProjects ep
            //            ON ep.ProjectID = p.ProjectID
            //        JOIN Employees e
            //            ON e.EmployeeID = ep.EmployeeID
            //      WHERE e.FirstName = @EmployeeFirstName AND e.LastName = @EmployeeLastName
            //END


            //GO
    }
}
