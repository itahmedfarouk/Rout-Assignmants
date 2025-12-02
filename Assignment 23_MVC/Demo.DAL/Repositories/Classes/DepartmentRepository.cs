using Demo.DAL.Data.DBContexts;
using Demo.DAL.Models.DepartmentModel;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Classes
{
	// primary Constructor prevent Dependency Injection
	public class DepartmentRepository(ApplicationDBContext _context) :GenericRepository<Department>(_context), IDepartmentRepository
	{
	}
}
