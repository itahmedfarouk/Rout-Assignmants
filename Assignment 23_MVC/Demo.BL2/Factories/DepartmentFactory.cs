using Demo.BL2.DTOS.Department;
using Demo.DAL.Models.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL2.Factories
{
	public static class DepartmentFactory
	{
		public static DepartmentDto DepartmentToDepartmentDto(this Department department)
		{
			if (department == null) return null;
			return new DepartmentDto
			{
				DeptId = department.Id,
				Name = department.Name,
				Code = department.Code,
				Description = department.Description,
				DateOfCreation = DateOnly.FromDateTime(department.CreatedOn)
			};
		}
		public static Department DepartmentDtoToDepartment(this DepartmentDto departmentDto)
		{
			if (departmentDto == null) return null;
			return new Department
			{
				Id = departmentDto.DeptId,
				Name = departmentDto.Name,
				Code = departmentDto.Code,
				Description = departmentDto.Description,
				CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly(0, 0))
			};
		}
		public static DepartmentDetailsDto DepartmentToDepartmentDetailsDto(this Department department)
		{
			if (department == null) return null;
			return new DepartmentDetailsDto()
			{
				Id = department.Id,
				Name = department.Name,
				Code = department.Code,
				Description = department.Description,
				DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
				DateOfLastModification = DateOnly.FromDateTime(department.LastModifiedOn),
				CreatedBy = department.CreatedBy,
				LastModifiedBy = department.LastModifiedBy,
				IsDeleted = department.IsDeleted
			};
		}
		public static Department ToEntity(this CreatedDepartmentDto dto)
		{
			return new Department
			{
				Name = dto.Name,
				Code = dto.Code,
				Description = dto.Description,
				CreatedOn = dto.DateOfCreation.ToDateTime(new TimeOnly(0, 0))
			};

		}
		public static Department ToEntity(this UpdatedDepartmentDto dto)
		{
			return new Department
			{
				Id = dto.Id,
				Name = dto.Name,
				Code = dto.Code,
				Description = dto.Description,
				CreatedOn = dto.DateOfCreation.ToDateTime(new TimeOnly(0, 0))
			};
		}
	}
}
