using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment02_Ef_Core.Models
{
    [Table("Department")]
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Head/Manager instructor (optional)
        public int? Ins_ID { get; set; }
        public DateTime HiringDate { get; set; }

        // Navigations
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public Instructor? Manager { get; set; }
    }
}
