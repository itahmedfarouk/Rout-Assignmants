using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment02_Ef_Core.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string Adress { get; set; } = string.Empty;
        public decimal HourRateBouns { get; set; }
        public int Dept_ID { get; set; }

        // Navigations
        public Department? Department { get; set; }
        public Department? ManagedDepartment { get; set; }
        public ICollection<Course_Inst> Course_Insts { get; set; } = new List<Course_Inst>();
    }
}
