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
    public class StudCourseConfig : IEntityTypeConfiguration<Stud_Course>
    {
        public void Configure(EntityTypeBuilder<Stud_Course> entity)
        {
            entity.ToTable("Stud_Course");
            entity.HasKey(sc => new { sc.stud_ID, sc.Course_ID });

            entity.Property(sc => sc.Grade).HasColumnType("decimal(5,2)");

            entity.HasOne(sc => sc.Student)
                  .WithMany(s => s.Stud_Courses)
                  .HasForeignKey(sc => sc.stud_ID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sc => sc.Course)
                  .WithMany(c => c.Stud_Courses)
                  .HasForeignKey(sc => sc.Course_ID)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
