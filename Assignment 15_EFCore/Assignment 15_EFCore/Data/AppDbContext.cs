using Assignment01_Ef_Core.Configurations;
using Assignment01_Ef_Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Assignment01_Ef_Core
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<Stud_Course> Stud_Courses => Set<Stud_Course>();
        public DbSet<Course_Inst> Course_Insts => Set<Course_Inst>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=AssignmentDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InstructorConfig());
            modelBuilder.ApplyConfiguration(new StudCourseConfig());
            modelBuilder.ApplyConfiguration(new CourseInstConfig());
        }
    }
}
