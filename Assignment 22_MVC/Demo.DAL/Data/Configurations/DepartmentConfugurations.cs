using Demo.DAL.Models.DepartmentModel;

namespace Demo.DAL.Data.Configurations
{
    public class DepartmentConfugurations :BaseEntityConfigurations<Department>,  IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(20)");
            builder.Property(D => D.Code).HasColumnType("varchar(20)");
            builder.Property(D => D.Description).HasColumnType("varchar(200)");

           

            base.Configure(builder); // same builder of Department
        }
    }
}
