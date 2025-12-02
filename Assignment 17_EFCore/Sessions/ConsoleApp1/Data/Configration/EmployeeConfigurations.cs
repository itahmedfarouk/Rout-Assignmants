using ConsoleApp1.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Data.Configration
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //// Refactor Employee For Another Overload APIs[Lambda Expression]

            //builder.Property<string>("Address");
            //builder.Property(nameof(Employee.Name));
            builder.Property(P => P.Name) // Lambda Expression
                                           .HasColumnType("varchar")
                                           .HasMaxLength(40)
                                           .IsRequired();
            //builder.HasKey(E => new { E.Id, E.Name });// Composite PK
            builder.ToTable("Employees", "HR");
        }
    }
}
