using MVC_Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Company.Data
{
    public class DBInitializer
    {
        public static void Initialize(EmployeeContext context)
        {
            context.Database.EnsureCreated();
            if (context.Employees.Any())
            {
                return;
            }

            var employees = new Employee[]
            {
              new Employee{IdEmployee=1,Name="Alexander",Intro="Lorem ipsum"},
              new Employee{IdEmployee = 2, Name = "Mark", Intro = "Lorem ipsum"}

            };
            foreach (Employee s in employees)
            {
                context.Employees.Add(s);
            }
            context.SaveChanges();

        }
    }
}
