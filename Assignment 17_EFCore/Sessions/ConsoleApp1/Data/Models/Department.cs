using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Data.Models
{
    public class Department
    {

        public int DepartmentId { get; set; } // Identity

        public required string Name { get; set; } // Required Refernce Type
        public DateOnly CreationDate { get; set; }

        #region Work Relationship (1 To Many => Take The PK Of One As FK In Many)
        public virtual List<Employee> Employees { get; set; } // Navigational Property 


        #endregion


        #region Manage Relationship (1 to 1 => take the pk of optional as fk in mandatory)
        ////[ForeignKey("Manger")]
        // same as 
        [ForeignKey(nameof(Manger))]
        public int ManagerId { get; set; } // FK
        [InverseProperty(nameof(Employee.DepartmentToManage))]
        public virtual Employee Manger { get; set; }

        #endregion
    }
}
