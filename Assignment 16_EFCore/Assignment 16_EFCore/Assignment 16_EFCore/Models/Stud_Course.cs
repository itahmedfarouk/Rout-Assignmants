using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment02_Ef_Core.Models
{
    public class Stud_Course
    {
        public int stud_ID { get; set; }
        public int Course_ID { get; set; }
        public decimal Grade { get; set; }

        // Navigations
        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}
