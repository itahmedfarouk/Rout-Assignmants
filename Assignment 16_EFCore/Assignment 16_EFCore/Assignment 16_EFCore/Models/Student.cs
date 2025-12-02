using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment02_Ef_Core.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Dep_Id { get; set; }

        // Navigation
        public Department? Department { get; set; }

        // Many-to-Many (payload) with Course
        public ICollection<Stud_Course> Stud_Courses { get; set; } = new List<Stud_Course>();
    }
}
