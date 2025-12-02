


using Demo.DAL.Data.Configurations;
using Demo.DAL.Models.DepartmentModel;

namespace Demo.DAL.Data.Configuration
{
	internal class DepartmentConfiguration : BaseEntityConfiguration<Department>,IEntityTypeConfiguration<Department>
	{
		public new void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.HasKey(d => d.Id);
			builder.Property(d => d.Id).UseIdentityColumn(10,10);
			builder.Property(d => d.Name).HasColumnType("varchar(20)");
			builder.Property(d => d.Code).HasColumnType("varchar(20)");
			builder.Property(d => d.Description).HasColumnType("varchar(200)");

			base.Configure(builder);

		}
	
	}
}
