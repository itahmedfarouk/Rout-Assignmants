using Demo.BLL.DTOs.DepartmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Interfaces
{
    public interface IDepartmentServices
    {
        int AddDepartment(AddDepartmentDto dto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DetailedDepartmentDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDto dto);
    }
}
