using Demo.DAL.Data.DBContexts;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Classes
{
	public class UnitOfWork:IUnitOfWork
	{
		public readonly IEmployeeRepository _Employees;
		public readonly IDepartmentRepository _Departments;
		public readonly ApplicationDBContext _dbContexts;


		public UnitOfWork(IDepartmentRepository department,IEmployeeRepository employee,ApplicationDBContext dBContext) {
			_Employees = employee;
			_Departments = department;
			_dbContexts = dBContext;
		}

		public IEmployeeRepository Employees => _Employees;

		public IDepartmentRepository Departments => _Departments;

		public int SaveChanges()
		{
			return _dbContexts.SaveChanges();
		}
	}
}
