using Assignment02_Ef_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment02_Ef_Core.Configurations
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.ToTable("Department");
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Name).HasMaxLength(100).IsRequired();
            entity.Property(d => d.HiringDate).HasColumnType("date");

            entity.HasOne(d => d.Manager)
                  .WithOne(i => i.ManagedDepartment)
                  .HasForeignKey<Department>(d => d.Ins_ID)
                  .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
