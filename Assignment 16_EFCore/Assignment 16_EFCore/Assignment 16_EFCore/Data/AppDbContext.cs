using Assignment02_Ef_Core.Configurations;
using Assignment02_Ef_Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Assignment02_Ef_Core.Data
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
                optionsBuilder.UseSqlServer("Server=AHMED-HISHAM;Database=AssignmentEF-Core02;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new InstructorConfig());
            modelBuilder.ApplyConfiguration(new TopicConfig());
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new StudCourseConfig());
            modelBuilder.ApplyConfiguration(new CourseInstConfig());
        }
    }
}
