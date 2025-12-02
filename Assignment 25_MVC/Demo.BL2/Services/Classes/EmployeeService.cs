using AutoMapper;
using Demo.BL2.DTOS.Employee;
using Demo.BL2.Services.AttachmentService;
using Demo.BL2.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL2.Services.Classes
{
	public class EmployeeService(IUnitOfWork _unitOfWork,IMapper _mapper,IAttachmentService _attachmentService) : IEmployeeService
	{
		public int AddEmployee(CreatedEmployeeDto createdEmployeeDto)
		{
			var employee = _mapper.Map<Employee>(createdEmployeeDto);
			if (createdEmployeeDto.image is not null) {
				var image = _attachmentService.UploadAttachment(createdEmployeeDto.image, "Images");
				employee.ImageName = image;
			}
			_unitOfWork.Employees.Add(employee);
			return _unitOfWork.SaveChanges();
		}

		public IEnumerable<EmployeeDto> GetAllEmployee(string? EmployeeSearchName, bool WithTracking = false)
		{

			#region OldImplemntation
			//var employees = _employeeRepository.GetAll(WithTracking);
			//return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
			#endregion

			#region NewImplementation
			// Use filtering in repository to optimize data retrieval
			if (!string.IsNullOrWhiteSpace(EmployeeSearchName))
			{
				var employees = _unitOfWork.Employees.GetAllWithFilter(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()), WithTracking);
				return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
			}
			else
			{
				var employees = _unitOfWork.Employees.GetAll(WithTracking);
				return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
			}
			#endregion



			// Or use expression overloading to optimize data retrieval
			#region ExpressionOverloadingGetall
			//var employees = _employeeRepository.GetAll<EmployeeDto>(e => new EmployeeDto
			//{
			//	Id = e.Id,
			//	Name = e.Name,
			//	Email = e.Email,
			//	Salary = e.Salary,
			//	Age = e.Age,
			//	Gender = e.Gender.ToString(),
			//	EmployeeType = e.EmployeeType.ToString(),
			//	IsActive = e.IsActive
			//});
			//return employees;
			#endregion


		}

		public EmployeeDetailsDto GetEmployeeById(int? id)
		{
			var employee = _unitOfWork.Employees.GetById(id);
			return (employee is null)? null: _mapper.Map<EmployeeDetailsDto>(employee);
		}

		public bool RemoveEmployee(int id)
		{
			var employee = _unitOfWork.Employees.GetById(id);
			if (employee is null) return false;
			else
			{
				employee.IsDeleted=true;
				_unitOfWork.Employees.Update(employee);
				return _unitOfWork.SaveChanges()>0 ? true : false;
			}
		}

		public int Update(UpdatedEmployeeDto updatedEmployeeDto)
		{
			var updatedemp= _mapper.Map<Employee>(updatedEmployeeDto);
			_unitOfWork.Employees.Update(updatedemp);
			return _unitOfWork.SaveChanges();
		}
	}
}
