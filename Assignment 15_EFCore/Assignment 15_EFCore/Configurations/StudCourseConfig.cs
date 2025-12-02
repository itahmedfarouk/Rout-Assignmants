using Assignment01_Ef_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01_Ef_Core.Configurations
{
    public class StudCourseConfig : IEntityTypeConfiguration<Stud_Course>
    {
        public void Configure(EntityTypeBuilder<Stud_Course> entity)
        {
            entity.ToTable("Stud_Course");
            entity.HasKey(e => new { e.stud_ID, e.Course_ID });

            entity.Property(e => e.Grade)
                  .HasColumnType("decimal(5,2)");
        }
    }
}
