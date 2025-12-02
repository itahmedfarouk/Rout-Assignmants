using Demo.DAL.Models.DepartmentModel;
using Demo.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.DBContexts
{
	public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
	{
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("Server=.;Database=DemoDB;Trusted_Connection=True;TrustServerCertificate=True;");
		//}
		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}


	}
}
