using Demo.BL2.DTOS.Department;
using Demo.BL2.Factories;
using Demo.BL2.Services.Interfaces;
using Demo.DAL.Data.DBContexts;
using Demo.DAL.Models;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL2.Services.Classes
{
	public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
	{
		public IEnumerable<DepartmentDto> GetAll()
		{
			var departments = _unitOfWork.Departments.GetAll();
			// manual mapping
			//var departmentDtos = departments.Select(d => new DepartmentDto
			//{
			//	DeptId = d.Id,
			//	Name = d.Name,
			//	Code = d.Code,
			//	Description = d.Description,
			//	DateOfCreation = DateOnly.FromDateTime(d.CreatedOn)
			//});
			// using factory method
			var departmentDtos = departments.Select(d => d.DepartmentToDepartmentDto());
			return departmentDtos;
		}

		public DepartmentDetailsDto GetById(int? id)
		{

			var department = _unitOfWork.Departments.GetById(id);
			if (department == null)
			{
				return null;
			}
			// manual mapping
			//var departmentDetailsDto = new DepartmentDetailsDto
			//{
			//	Id = department.Id,
			//	Name = department.Name,
			//	Code = department.Code,
			//	Description = department.Description,
			//	DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
			//	DateOfLastModification = DateOnly.FromDateTime(department.LastModifiedOn),
			//	CreatedBy = department.CreatedBy,
			//	LastModifiedBy = department.LastModifiedBy,
			//	IsDeleted = department.IsDeleted
			//};
			//return departmentDetailsDto;
			// using constructor
			//return new DepartmentDetailsDto(department);
			// using factory method
			return department.DepartmentToDepartmentDetailsDto();
		}
		public int Add(CreatedDepartmentDto createdDepartmentDto)
		{
			_unitOfWork.Departments.Add(createdDepartmentDto.ToEntity());
			return _unitOfWork.SaveChanges();
		}
		public int Update(UpdatedDepartmentDto updatedDepartmentDto)
		{
			_unitOfWork.Departments.Update(updatedDepartmentDto.ToEntity());
			return _unitOfWork.SaveChanges();
		}
		public bool Remove(int id)
		{
			var department = _unitOfWork.Departments.GetById(id);
			if (department == null) return false;
			else
			{
				_unitOfWork.Departments.Remove(department);

				return _unitOfWork.SaveChanges() > 0;
			}
		}
	}
}
