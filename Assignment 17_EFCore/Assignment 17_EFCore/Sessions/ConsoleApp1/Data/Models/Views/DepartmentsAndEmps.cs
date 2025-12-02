using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Data.Models.Views
{
    public class DepartmentsAndEmps // Left outer Join
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string? EmployeeName { get; set; }
    }
}
