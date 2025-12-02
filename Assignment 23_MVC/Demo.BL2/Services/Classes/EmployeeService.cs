using AutoMapper;
using Demo.BL2.DTOS.Employee;
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
	public class EmployeeService(IEmployeeRepository _employeeRepository,IMapper _mapper) : IEmployeeService
	{
		public int AddEmployee(CreatedEmployeeDto createdEmployeeDto)
		{
			var employee = _mapper.Map<Employee>(createdEmployeeDto);
			return _employeeRepository.Add(employee);
		}

		public IEnumerable<EmployeeDto> GetAllEmployee(bool WithTracking = false)
		{
			var employees = _employeeRepository.GetAll(WithTracking);
			return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
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
			var employee = _employeeRepository.GetById(id);
			return (employee is null)? null: _mapper.Map<EmployeeDetailsDto>(employee);
		}

		public bool RemoveEmployee(int id)
		{
			var employee = _employeeRepository.GetById(id);
			if (employee is null) return false;
			else
			{
				employee.IsDeleted=true;
				return _employeeRepository.Update(employee)>0 ? true : false;
			}
		}

		public int Update(UpdatedEmployeeDto updatedEmployeeDto)
		{
			var updatedemp= _mapper.Map<Employee>(updatedEmployeeDto);
			return _employeeRepository.Update(updatedemp);
		}
	}
}
