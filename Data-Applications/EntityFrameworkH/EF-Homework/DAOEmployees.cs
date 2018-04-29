using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using EF_Homework;



    public static class DAOEmployees
    {
        //a class for inserting, finding by key, modifying and deleting an entity

        public static void Add(Employee employee)
        {
            using(var ctx = new SoftUniEntities())
            {
                var entry = ctx.Entry(employee);
                entry.State = EntityState.Added;

                ctx.Employees.Add(employee);                
                ctx.SaveChanges();
            }
        }

        public static Employee FindByKey(object key)
        {
            using( var ctx = new SoftUniEntities())
            {
                Employee employee = ctx.Employees.Find(key);

                return employee;
            }
        }

        public static void Modify(Employee employee, string newName)
        {
            using(var ctx = new SoftUniEntities())
            {
                //ctx.Employees.Attach(employee);

                string[] name = Regex.Split(newName.Trim(), @"\s+");
                string firstName = Regex.Split(newName.Trim(), @"\s+")[0];
                employee.FirstName = firstName;

                if (name.Length == 2)
                {
                    string lastName = Regex.Split(newName.Trim(), @"\s+")[0];
                    employee.LastName = lastName;
                }

                var entry = ctx.Entry(employee);
                entry.State = EntityState.Modified;

                ctx.SaveChanges();
            }            
        }

        public static void Delete(Employee employee)
        {
            using(var ctx = new SoftUniEntities())
            {
                ctx.Entry(employee).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }
    }
