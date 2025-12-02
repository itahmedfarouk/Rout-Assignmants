using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configurations
{
	public class EmployeeConfigurations:BaseEntityConfiguration<Employee>,IEntityTypeConfiguration<Employee>
	{
		public new void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Name).HasColumnType("varchar(50)").IsRequired();
			builder.Property(e => e.address).HasColumnType("varchar(150)");
			builder.Property(e => e.IsActive).HasDefaultValue(true);
			builder.Property(e => e.Salary).HasColumnType("decimal(10,2)").IsRequired();
			builder.Property(e => e.Email).HasColumnType("varchar(100)");
			builder.Property(e => e.Phone).HasColumnType("varchar(15)");
			builder.Property(e => e.HiringDate).HasDefaultValueSql("GETDATE()").IsRequired();
			builder.Property(e => e.Gender).HasConversion((empgender) => empgender.ToString(), (gender) => (Gender)Enum.Parse(typeof(Gender), gender));
			builder.Property(e => e.EmployeeType).HasConversion((emptype) => emptype.ToString(), (emptype) => (EmployeeType)Enum.Parse(typeof(EmployeeType), emptype));
			// RelationShip
			builder.HasOne(e => e.Department).WithMany(d => d.Employees).HasForeignKey(e => e.DepartmentId).OnDelete(DeleteBehavior.SetNull);
			base.Configure(builder);
		}
	}
}
