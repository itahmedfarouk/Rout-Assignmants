using ConsoleApp1.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Data.Configration
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder
                        .HasMany(D => D.Employees)
                        .WithOne(E => E.WorkDepartment)
                        .IsRequired(false)/*default is true*/
                        .HasForeignKey(E => E.DepartmentId)
                        .OnDelete(DeleteBehavior.NoAction);

            builder
                        .HasOne(D => D.Manger)
                        .WithOne(E => E.DepartmentToManage)
                        .IsRequired()
                        .HasForeignKey<Department>(D => D.ManagerId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
