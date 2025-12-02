using Demo.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories
{
    //Primary Constructor => .Net 8 Feature
    public class DepartmentRepository(ApplicationDbContext _context) : IDepartmentRepository
    //High Level Model
    {

        //CRUD
        //Get Department By Id
        //////ApplicationDbContext context = new ApplicationDbContext();//Low Level Model



        public Department? GetById(int id)
        {
            var deparment = _context.Departments.Find(id);
            return deparment;
        }
        //Get All Departments
        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _context.Departments.ToList();
            else
                return _context.Departments.AsNoTracking().ToList();
        }


        //Add Department
        public int Add(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();
        }

        //Update Department
        public int Update(Department department)
        {
            _context.Departments.Update(department);
            return _context.SaveChanges();
        }
        //Delete Department
        public int Delete(Department department)
        {
            _context.Departments.Remove(department);
            return _context.SaveChanges();
        }

        public int Remove(Department department)
        {
            throw new NotImplementedException();
        }
        //Assign New Manager For Department

    }
}
