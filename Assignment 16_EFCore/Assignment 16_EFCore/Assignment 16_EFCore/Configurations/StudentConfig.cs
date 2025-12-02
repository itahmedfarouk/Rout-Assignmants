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
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.ToTable("Student");
            entity.HasKey(s => s.Id);

            entity.Property(s => s.FName).HasMaxLength(100).IsRequired();
            entity.Property(s => s.LName).HasMaxLength(100).IsRequired();
            entity.Property(s => s.Address).HasMaxLength(200);

            entity.HasOne(s => s.Department)
                  .WithMany(d => d.Students)
                  .HasForeignKey(s => s.Dep_Id)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
