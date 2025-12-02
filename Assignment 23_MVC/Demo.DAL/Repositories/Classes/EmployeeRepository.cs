using Demo.DAL.Data.DBContexts;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Classes
{
	public class EmployeeRepository(ApplicationDBContext _context):GenericRepository<Employee>(_context), IEmployeeRepository
	{
	}
}
