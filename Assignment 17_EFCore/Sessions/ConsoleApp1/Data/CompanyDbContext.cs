using ConsoleApp1.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleApp1.Data.Configration;
using System.Reflection;
using ConsoleApp1.Data.Models.Mtom;
using ConsoleApp1.Data.Models.Views;
namespace ConsoleApp1.Data
{
    //static class SqlServerTypes //وانا شغال Types  بعمله عشان مغلطش ف ال 
    //{
    //    public static string varchar20 => "varchar(20)"; // For Return It But Cannot Set
    //}
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ////base.OnConfiguring(optionsBuilder);

            //// Old Version=> Connection String
            ////optionsBuilder.UseSqlServer("Data Source=AHMED-HISHAM;Initial Catalog=CompanyTest;Integrated Security=True");

            //// New Version => Connection String
            //optionsBuilder.UseSqlServer("Server=AHMED-HISHAM ; Database=CompanyTest1; Trusted_Connection=True; TrustServerCertificate=True");
            
            /// Using Lazy Loading
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=AHMED-HISHAM ; Database=CompanyTest1; Trusted_Connection=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///// Fluent APIs /////



            //base.OnModelCreating(modelBuilder);

            //// For Apply Fluent APIs- Configuration classes[Calling Manual]
            //modelBuilder.ApplyConfiguration/*<Employee>*/(new EmployeeConfigurations()); // Call Configure & Exceute 
            //modelBuilder.ApplyConfiguration/*<Product>*/(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration/*<Project>*/(new ProjectConfiguration());

            //// لو عندي كذا واحد مش لازم استدعيهم واحد ورا التاني ف هستخدم دي عشان تعملل كده لوحدها[Calling Automatic]
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Employee>()
                        .OwnsOne(E => E.EmpAddress, Address => Address.WithOwner());

            #region Work Relationship (1 to Many)


            //// Replace it in Configuration Class
            //modelBuilder.Entity<Employee>()
            //            .HasOne(E => E.WorkDepartment)
            //            .WithMany(D => D.Employees)
            //            .IsRequired()/*default is true*/
            //            .HasForeignKey(E => E.DepartmentId);





            #endregion
            #region Manage Relationship (1 to 1)
            //// Replace it in Configuration Class
            //modelBuilder.Entity<Employee>()
            //            .HasOne(E => E.DepartmentToManage)
            //            .WithOne(D => D.Manger)
            //            .IsRequired()
            //            .HasForeignKey<Department>(D => D.ManagerId);






            #endregion

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<DepartmentsAndEmps>()
                        .HasNoKey()
                        .ToView("DepartmentsAndEmps");

            //// بيستدعيهم هنا وبيعدل عليهم تبع الكلاس دا
            ////base.OnModelCreating(modelBuilder); /// Set Is When Avilable DbSet<>
            //// Fluent APIs For Every Model

            // {Shadow Proprty} => Not In Your .Net Class But Will Be In The Table 
            ////modelBuilder.Entity<Employee>().Property<string>("Address"); ///Address Not In Model  & Will Be In The Table 
            //modelBuilder.Entity<Employee>()
            //            .OwnsOne(e => e.Address);



            //// Same as But Using nameof
            //modelBuilder.Entity<Employee>().Property(nameof(Employee.Name));
            //modelBuilder.Entity<Employee>().Property(nameof(Employee.Address));

            //// Same as Butt Using Lambda Expression 
            ////modelBuilder.Entity<Employee>().Property(P => P.Name).HasColumnType(SqlServerTypes.varchar20);
            //modelBuilder.Entity<Employee>().Property(P => P.Name)
            //                               .HasColumnType("varchar")
            //                               .HasMaxLength(40)
            //                               .IsRequired();


            ////// Refactor Employee For Another Overload APIs[Lambda Expression]
            //modelBuilder.Entity<Employee>(EntityBuilder =>
            //{
            //    EntityBuilder.Property<string>("Address");
            //    //EntityBuilder.Property(nameof(Employee.Name));
            //    EntityBuilder.Property(P => P.Name) // Lambda Expression
            //                               .HasColumnType("varchar")
            //                               .HasMaxLength(40)
            //                               .IsRequired();
            //});





            ////// For Added In Class Property In Another Solution[Session 1 Linq]
            //modelBuilder.Entity<Product>().ToTable("Products", "dbo");
            //modelBuilder.Entity<Product>().HasKey(nameof(Product.ProductID)); // Code [Key]

            ////// For Make Composite Primary Key => [Anonymous Object]
            ////modelBuilder.Entity<Product>().HasKey(P => new { P.ProductID, P.ProductName });


            //modelBuilder.Entity<Product>().Property(P => P.ProductName)
            //                              .HasColumnName("Name")
            //                              .HasColumnType("varchar(30)")
            //                              .IsRequired()
            //                              .HasAnnotation("MaxLength", 30);


            //// Refactor Product For Another Overload APIs[Lambda Expression]
            //modelBuilder.Entity<Product>(ProductBuilder =>
            //{
            //    ////// For Added In Class Property In Another Solution[Session 1 Linq]
            //ProductBuilder.ToTable("Products", "dbo");
            //ProductBuilder.HasKey(nameof(Product.ProductID)); // Code [Key]

            ////// For Make Composite Primary Key => [Anonymous Object]
            ////ProductBuilder.HasKey(P => new { P.ProductID, P.ProductName });
            //ProductBuilder.Property(P => P.ProductName)
            //                              .HasColumnName("Name")
            //                              .HasColumnType("varchar(30)");
            //                              //.IsRequired()
            //                              //.HasAnnotation("MaxLength", 30);
            //});



            //// Refactor Project For Another Overload APIs[Lambda Expression]
            //modelBuilder.Entity<Project>()
            //            .Property(P => P.CreationDate)
            //            ////.HasDefaultValue(DateOnly.FromDateTime(DateTime.Now)); // Get In Migration Time
            //            //.HasDefaultValueSql("GETDATE()"); // Get Date Now By Sql 
            //            .HasComputedColumnSql("GETDATE()");




        }



        ////اني عندي جداول DB  عشان اعرف ال 
        //public DbSet</*Entity => Strucure From Class*/ Employee> /*Table Name*/ Employees{ get; set; }// For Explain Only
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<DepartmentsAndEmps> DepartmentsAndEmps { get; set; }


    }
}
