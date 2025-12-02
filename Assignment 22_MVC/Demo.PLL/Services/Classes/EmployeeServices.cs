using AutoMapper;
using Demo.BLL.DTOs.EmployeeDtos;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Classes
{
    public class EmployeeServices(IEmployeeRepository _employeeRepository, IMapper _mapper) : IEmployeeServices
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking = false)
        {
            var employees = _employeeRepository.GetAll();
            //////// Not Using AutoMapper
            ////var employeesDto = employees.Select(emp => new EmployeeDto
            ////{
            ////    Id = emp.Id,
            ////    Name = emp.Name,
            ////    Age = emp.Age,
            ////    Email = emp.Email,
            ////    EmployeeType = emp.EmployeeType.ToString(),
            ////    Gender = emp.Gender.ToString(),
            ////    IsActive = emp.IsActive,
            ////    Salary = emp.Salary,
            ////});
            ////USing AutoMapper
            ////                            Destination Source
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);


        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<EmployeeDetailsDto>(employee);
            //////Not Using AutoMapper
            ////var employeeDto = new EmployeeDetailsDto()
            ////{
            ////    Id = employee.Id,
            ////    Name = employee.Name,
            ////    Age = employee.Age,
            ////    Address = employee.Address,
            ////    Email = employee.Email,
            ////    EmployeeType = employee.EmployeeType.ToString(),
            ////    Gender = employee.Gender.ToString(),
            ////    IsActive=employee.IsActive,
            ////    Salary = employee.Salary,
            ////    PhoneNumber= employee.PhoneNumber,
            ////    CreatedBy= employee.CreatedBy,
            ////    CreatedOn= employee.CreatedOn,
            ////    HiringDate=DateOnly.FromDateTime(employee.HiringDate),
            ////    LastModifiedBy= employee.LastModifiedBy,
            ////    LastModifiedOn= employee.LastModifiedOn,
            ////};


        }
        public int AddEmployee(CreateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            return _employeeRepository.Add(employee);

        }

        public int UpdateEmployee(UpdateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            return _employeeRepository.Update(employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null)
                return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) > 0 ? true : false;
            }
        }




    }
}
