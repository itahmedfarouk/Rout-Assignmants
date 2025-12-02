using ConsoleApp1.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Data.DataSeeding
{
    public static class CompanyDbContextSeed
    {
        public static void Seed(CompanyDbContext dbContext)
        {

            if (!dbContext.Employees.Any())
            {
                var employeesData = File.ReadAllText("E:\\Route (FullStack .Net)\\Ef-Core Coding Sessions\\Session 1 & 2\\ConsoleApp1\\Data\\DataSeeding\\employees.json");
                //// Seed Operation Just Department
                var employees = JsonSerializer.Deserialize<List<Employee>>(employeesData);  //Convert JSON To Object

                foreach (var emp in employees)
                {
                    dbContext.Employees.Add(emp);
                }
                dbContext.SaveChanges();
            }


            if (!dbContext.Departments.Any())
            {
                var departmentsData = File.ReadAllText("E:\\Route (FullStack .Net)\\Ef-Core Coding Sessions\\Session 1 & 2\\ConsoleApp1\\Data\\DataSeeding\\departments.json");
                //// Seed Operation Just Department
                var departments = JsonSerializer.Deserialize<List<Department>>(departmentsData);  //Convert JSON To Object

                foreach (var dept in departments)
                {
                    dbContext.Departments.Add(dept);
                }
                dbContext.SaveChanges();
            }

        }
    }
}
