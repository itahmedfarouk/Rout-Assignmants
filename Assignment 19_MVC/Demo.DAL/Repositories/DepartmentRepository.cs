using Demo.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories
{
    //Primary Constructor => .Net 8 Feature
    internal class DepartmentRepository(ApplicationDbContext context) //High Level Model
    {



        //CRUD
        //Get Department By Id
        ////ApplicationDbContext context = new ApplicationDbContext();//Low Level Model



        private readonly ApplicationDbContext _context = context; // Default => Null
        public Department? GetById(int id )
        {
            var deparment = _context.Departments.Find(id);   
            return deparment;
        }
        //Get All Departments
        //Add Department
        //Update Department
        //Delete Department
        //Assign New Manager For Department
    }
}
