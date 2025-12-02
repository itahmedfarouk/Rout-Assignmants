using Demo.DAL.Data.Contexts;
using Demo.DAL.Models.DepartmentModel;
using Demo.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Demo.DAL.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContext _dbcontext) : GenericRepository<Department>(_dbcontext) ,IDepartmentRepository
    {
      

    }
}
