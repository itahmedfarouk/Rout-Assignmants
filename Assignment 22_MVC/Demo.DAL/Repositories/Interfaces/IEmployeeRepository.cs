using Demo.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {

    }
}
