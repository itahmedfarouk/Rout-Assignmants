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
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> entity)
        {
            entity.ToTable("Course");
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name).HasMaxLength(150).IsRequired();
            entity.Property(c => c.Description).HasMaxLength(500);

            entity.HasOne(c => c.Topic)
                  .WithMany(t => t.Courses)
                  .HasForeignKey(c => c.Top_Id)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
