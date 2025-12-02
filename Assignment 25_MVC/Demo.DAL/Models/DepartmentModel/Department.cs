using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models.DepartmentModel
{
	public class Department: BaseEntity
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public string? Description { get; set; }

		public virtual ICollection<Employee> Employees { get; set; }

		}
}
