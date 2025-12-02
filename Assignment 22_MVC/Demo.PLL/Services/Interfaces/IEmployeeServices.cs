using Demo.BLL.DTOs.DepartmentDtos;
using Demo.BLL.DTOs.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Interfaces
{
    public interface IEmployeeServices
    {
        IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking=false);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int AddEmployee(CreateEmployeeDto dto);
        int UpdateEmployee(UpdateEmployeeDto dto);
        bool DeleteEmployee(int id);
    }
}
