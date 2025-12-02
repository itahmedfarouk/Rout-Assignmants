using ConsoleApp1.Data;
using ConsoleApp1.Data.DataSeeding;
using ConsoleApp1.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Using Context

            /// Version 1 Old Version For Connect DB[Create Object]
            //CompanyDbContext context = new CompanyDbContext();
            //try
            //{

            //}
            //finally
            //{
            //    context.Dispose(); // For Close Connection DB
            //}


            //// Version 2 New Version FOr Connection DB[Create Object]
            //using (CompanyDbContext context = new CompanyDbContext())
            //{
            //    //Some Code
            //}


            //// Version 3 For Connection DB[Create Object] 
            /// using CompanyDbContext context = new CompanyDbContext(); // once end query Will Close Connection Automatic
            //context.Employees.Where(E => E.Id == 1).FirstOrDefault(); // For Execute Commands For DB
            //context.Database.Migrate(); // For Check Update DB 

            #endregion
            using CompanyDbContext context = new CompanyDbContext(); // once end query Will Close Connection Automatic
                                                                     //context.Employees.Where(E => E.Id == 1).FirstOrDefault(); // For Execute Commands For DB
                                                                     //context.Database.Migrate(); // For Check Update DB 
           
            #region CRUD Operation

            #region Add
            //// For Adding Employee In DB

            //Employee emp01 = new Employee() { Name = "Ahmed", Age = 22, Salary = 15000 };  // C# Object
            //Employee emp02 = new Employee() { Name = "Hisham", Age = 24, Salary = 20000 }; // C# Object

            //Console.WriteLine(context.Entry(emp01).State); //Detached
            //Console.WriteLine(context.Entry(emp02).State); //Detached
            //// دول كده لسه متضافوش لل DB


            /*context.Employees.Add(emp01);*/// When Available DbSet<> => // Insert Data From emp01 By VS
                                             ////context.Add(emp01);// دا اختصار للي فوقه
                                             ////context.Set<Employee>().Add(emp01); // When Not Available DbSet<>
                                             //context.Entry(emp01).State = EntityState.Added; // Added Manually


            //Console.WriteLine("After Added");
            //Console.WriteLine(context.Entry(emp01).State); //Added
            //Console.WriteLine(context.Entry(emp02).State); //Detached


            //context.Employees.Add(emp02); // Insert Data From emp02 By VS
            //context.SaveChanges(); // For Done Added Employees Table

            #endregion

            #region Retrevie
            //var employee = (from E in context.Employees
            //                where E.Id==1
            //                select E).FirstOrDefault();

            //if(employee is not null)
            //{
            //    Console.WriteLine(context.Entry(employee).State);//Unchanged => Because Just A Retrevie
            //    Console.WriteLine($"Id : {employee.Id}  ,  Name : {employee.Name} ");//Id : 1  ,  Name : Ahmed
            //}

            #region Get All
            //var employee = from E in context.Employees
            //                select E;

            ////// same as
            //var employee = context.Employees;
            //foreach(var item in employee)// Will Return All Names In Employee Table
            //{
            //    Console.WriteLine(item?.Name);//Hisham
            //}

            #endregion
            #endregion

            #region Update
            //var employee = (from E in context.Employees
            //                where E.Id == 1
            //                select E).FirstOrDefault();

            //if (employee is not null)
            //{
            //    Console.WriteLine(context.Entry(employee).State);//Unchanged

            //    employee.Name = "Hasan"; // From Ahmed To Hasan

            //    Console.WriteLine(context.Entry(employee).State);//Modified
            //    context.SaveChanges();
            //}
            #endregion

            #region Remove 
            //var employee = (from E in context.Employees
            //                where E.Id == 1
            //                select E).FirstOrDefault();

            //if (employee is not null)
            //{
            //    Console.WriteLine(context.Entry(employee).State);//Unchanged 

            //    //context.Employees.Remove(employee);// Using When Available DbSet
            //    ////same as but suger syntax
            //    //context.Remove(employee);

            //    //context.Set<Employee>().Remove(employee);// Using When Not Available DbSet
            //    context.Entry(employee).State = EntityState.Deleted;// Deleted Manually

            //    Console.WriteLine(context.Entry(employee).State);//Deleted
            //    context.SaveChanges();
            //}
            #endregion

            #endregion


            CompanyDbContextSeed.Seed(context);
            #region EFCORE Loading Instructions
            #region Loading Introduction


            //var employee01 = (from E in context.Employees // using SqlServer Profiler to Get Sql Query
            //                  where E.Id == 1
            //                  select E)/*.AsNoTracking().*/FirstOrDefault();
            //////select E).AsNoTracking().FirstOrDefault();// عشان لو عملت اي تغير بعدين ميقعدش مراقبها 
            //if (employee01 is not null)
            //{
            //    Console.WriteLine($"Id : {employee01.Id}  ,  Name : {employee01.Name} ");//Id : 1  ,  Name : Sama
            //    Console.WriteLine($"Work Department Name : {employee01.WorkDepartment?.Name ?? "- No Name -"}  ");
            //    Console.WriteLine($"Manage Department Name : {employee01.DepartmentToManage?.Name ?? "- No Name -"} ");
            //}


            //var employee02 = (from E in context.Employees
            //                  where E.Id == 2
            //                  select E).AsNoTracking().FirstOrDefault();

            //var employee02WorkDepartment = (from D in context.Departments
            //                                where D.DepartmentId == employee02.DepartmentId
            //                                select D).FirstOrDefault();

            //var employee02ManageDepartment = (from D in context.Departments
            //                                where D.ManagerId == employee02.Id
            //                                select D).FirstOrDefault();
            //if (employee02 is not null)
            //{
            //    Console.WriteLine($"Id : {employee02.Id}  ,  Name : {employee02.Name} ");//Id : 2  ,  Name : Nadia
            //    Console.WriteLine($"Work Department Name : {employee02WorkDepartment?.Name ?? "- No Name -"}  ");
            //    Console.WriteLine($"Manage Department Name : {employee02ManageDepartment?.Name ?? "- No Name -"} ");
            //} 





            #region Eager Loading
            //var employee01 = context.Employees.Where(E => E.Id == 1)
            //                                  .Include(E => E.DepartmentToManage)
            //                                  .Include(E=>E.WorkDepartment)
            //                                  .ThenInclude(D=>D.Manger)
            //                                  .FirstOrDefault();

            #endregion

            #region Explicit Loading
            //// Using Refernce
            //var employee01 = context.Employees.Where(E => E.Id == 1)
            //                                  .FirstOrDefault();
            //if (employee01 is not null)
            //{
            //    context.Entry(employee01).Reference(E=> E.WorkDepartment).Load();
            //    context.Entry(employee01).Reference(E=> E.DepartmentToManage).Load();
            //}


            //// Using Collections
            //var department05 = context.Departments.Where(D=>D.DepartmentId==5).FirstOrDefault();
            //context.Entry(department05).Collection(D => D.Employees).Load();
            //context.Entry(department05).Reference(D => D.Manger).Load();

            #endregion

            #region Lazy Loading
            //var emp=context.Employees.Where(E => E.Id ==1).FirstOrDefault();

            //Console.WriteLine($"Employee :: {emp.Name} ,  Work At :: {emp.WorkDepartment?.Name} ");//Employee :: Sama ,  Work At :: Markting
            #endregion

            #region Joins (Join , GroupJoin)
            //outer collection(prinipal table)    inner collection
            //var res = context.Employees.Join(context.Departments, e => e.Id, d => d.MangerId ,(emp, dept)=>new {emp,dept} );

            //foreach (var item in res)
            //{
            //    Console.WriteLine($"Employee :: {item.emp.Name} ,, Department :: {item.dept.Name}");   
            //}


            //var res = context.Departments.Join(context.Employees, d => d.DepartmentId, e => e.DepartmentId, (dept, emp) =>
            //new
            //{
            //    dept = dept,
            //    emp = emp,
            //});//Work Relation


            //res= from d in context.Departments
            //     join e in context.Employees
            //     on d.DepartmentId equals e.DepartmentId
            //     select new
            //     {
            //         dept = d,
            //         emp = e,
            //     };//Work Relation


            //foreach (var item in res)
            //{
            //    Console.WriteLine($"Employee :: {item.emp.Name} ,, Department :: {item.dept.Name}");
            //}




            //var res = context.Departments.GroupJoin(context.Employees, d => d.DepartmentId, e => e.DepartmentId,
            //    (dept, employees) => new
            //    {
            //        dept,
            //        employees
            //    }
            //);

            //foreach (var item in res)
            //{
            //    Console.WriteLine($"Department :: {item.dept.Name}");
            //    foreach (var emp in item.employees)
            //    {
            //        Console.WriteLine($"................. {emp.Name}");
            //    }
            //}

            //var res = context.Departments.GroupJoin(context.Employees,
            //    d => d.DepartmentId,
            //    e => e.DepartmentId,
            //    (department, employees) => new
            //    {
            //        department,
            //        employees=employees.DefaultIfEmpty()
            //    }).SelectMany(group=>group.employees , (group , emp) => new 
            //    {
            //        department=group.department,
            //        emp
            //    });

            //foreach (var item in res)
            //{
            //    Console.WriteLine($" Dept Id :: {item.department?.DepartmentId} , Dept Name :: {item.department?.Name} , Emp Name :: {item.emp?.Name}");
            //}


            //Cross Join

            //var res = from d in context.Departments
            //          from e in context.Employees
            //          select new
            //          {
            //              d,
            //              e
            //          };


            //foreach (var item in res)
            //{
            //    Console.WriteLine($" Dept :: {item.d.Name} , Emp :: {item.e.Name}");
            //}


            #endregion
            #endregion
            #endregion

            #region Mapping Views

            var data = context.DepartmentsAndEmps.ToList();
            foreach (var item in data)
            {
                Console.WriteLine($"{item.DepartmentName} , {item?.EmployeeName}");
            } 
            #endregion
        }
    }
}
