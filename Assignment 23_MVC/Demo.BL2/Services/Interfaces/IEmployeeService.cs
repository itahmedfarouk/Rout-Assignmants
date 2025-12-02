using Demo.BL2.DTOS.Department;
using Demo.BL2.DTOS.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL2.Services.Interfaces
{
	public interface IEmployeeService
	{
		int AddEmployee(CreatedEmployeeDto createdEmployeeDto);
		IEnumerable<EmployeeDto> GetAllEmployee( bool WithTracking = false);
		EmployeeDetailsDto GetEmployeeById(int? id);
		bool RemoveEmployee(int id);
		int Update(UpdatedEmployeeDto updatedEmployeeDto);
	}
}
