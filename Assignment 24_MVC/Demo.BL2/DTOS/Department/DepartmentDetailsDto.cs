using Demo.DAL.Models.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL2.DTOS.Department
{
	public class DepartmentDetailsDto
	{
		public DepartmentDetailsDto(Demo.DAL.Models.DepartmentModel.Department department)
		{
			Id = department.Id;
			Name = department.Name;
			Code = department.Code;
			Description = department.Description;
			DateOfCreation = DateOnly.FromDateTime(department.CreatedOn);
			DateOfLastModification = DateOnly.FromDateTime(department.LastModifiedOn);
			CreatedBy = department.CreatedBy;
			LastModifiedBy = department.LastModifiedBy;
			IsDeleted = department.IsDeleted;
		}
		public DepartmentDetailsDto() { }
		public int Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public DateOnly DateOfCreation { get; set; }

		public DateOnly DateOfLastModification { get; set; }
		public int CreatedBy { get; set; }
		public int LastModifiedBy { get; set; }

		public bool IsDeleted { get; set; }

	}
}
