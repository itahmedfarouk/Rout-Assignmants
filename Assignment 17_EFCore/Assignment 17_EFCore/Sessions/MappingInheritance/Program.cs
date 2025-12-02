using MappingInheritance.Data;
using MappingInheritance.Data.Models;

namespace MappingInheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using MyCompanyContext myCompanyContext = new MyCompanyContext();

            var emp01 = new FullTimeEmployee()
            {
                Name = "Rana",
                Age =20,
                Address = "Mansoura",
                Salary=3000,
                StartDate = DateTime.Now
            };

            var emp02 = new PartTimeEmployee()
            {
                Name = "Mai",
                Age = 30,
                Address = "Giza",
                CountOfHrs =15,
                HourRate = 250
            };

            /// For Adding In Tables///
            //myCompanyContext.Employees.Add(emp01);
            //myCompanyContext.Employees.Add(emp02);

            //myCompanyContext.SaveChanges();

            var res = myCompanyContext.Employees.ToList();
            if (res is not null)
            {
                foreach (var item in res.OfType<FullTimeEmployee>())
                {
                    Console.WriteLine($"{item.Name} , {item.Salary}"); //Rana , 3000.00
                }

                foreach (var item in res.OfType<PartTimeEmployee>())
                {
                    Console.WriteLine($"{item.Name} , {item.HourRate}"); //Mai , 250.00
                }
            }

           

            //var res = myCompanyContext.Employees.ToList();
            //if (res is not null)
            //{
            //    foreach (var item in res.OfType<PartTimeEmployee>())
            //    {
            //        Console.WriteLine($"{item.Name} , "); //Rana
            //    }
            //}

            //var fullTimeEmp = myCompanyContext.FullTimeEmployees.ToList().FirstOrDefault();
            //if (fullTimeEmp is not null)
            //{
            //    Console.WriteLine(fullTimeEmp.Name); //Rana
            //}
            //var partTimeEmp = myCompanyContext.PartTimeEmployees.ToList().FirstOrDefault();
            //if (partTimeEmp is not null)
            //{
            //    Console.WriteLine(partTimeEmp.Name); //Mai
            //}
        }
    }
}
