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
    public class InstructorConfig : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> entity)
        {
            entity.ToTable("Instructor");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Salary).HasColumnType("decimal(10,2)");
            entity.Property(e => e.Adress).HasMaxLength(200);
            entity.Property(e => e.HourRateBouns).HasColumnType("decimal(8,2)");

            entity.HasOne(e => e.Department)
                  .WithMany(d => d.Instructors)
                  .HasForeignKey(e => e.Dept_ID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
