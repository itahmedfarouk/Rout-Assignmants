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
    public class CourseInstConfig : IEntityTypeConfiguration<Course_Inst>
    {
        public void Configure(EntityTypeBuilder<Course_Inst> entity)
        {
            entity.ToTable("Course_Inst");
            entity.HasKey(ci => new { ci.inst_ID, ci.Course_ID });

            entity.HasOne(ci => ci.Instructor)
                  .WithMany(i => i.Course_Insts)
                  .HasForeignKey(ci => ci.inst_ID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ci => ci.Course)
                  .WithMany(c => c.Course_Insts)
                  .HasForeignKey(ci => ci.Course_ID)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
