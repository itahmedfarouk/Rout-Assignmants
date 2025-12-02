using Demo.DAL.Models.DepartmentModel;
using Demo.DAL.Models.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models.EmployeeModel
{
	public class Employee:BaseEntity
	{
		public string Name { get; set; }=null!;
		public int Age { get; set; }
		public string? address { get; set; }
		public bool IsActive { get; set; }
		public decimal Salary { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public DateTime HiringDate { get; set; }
		public Gender Gender { get; set; }
		public EmployeeType EmployeeType { get; set; }

		public int? DepartmentId { get; set; }
		public virtual Department? Department { get; set; }

	}
}
