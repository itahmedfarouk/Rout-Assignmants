using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Data.Models.Mtom
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public List<Student> Students { get; set; }//Old
        //public List<StudentCourse> StudentsCourses { get; set; }//New
    }
}
