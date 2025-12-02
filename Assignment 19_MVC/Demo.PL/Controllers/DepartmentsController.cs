using Demo.BLL;
namespace Demo.PL.Controllers
{
    public class DepartmentsController
    {
        // DepartmentServices Used Across All Actions
        //EmployeeService => Assign Manager : This Services Nedded Only One Action 
        public DepartmentsController(DepartmentServices departmentService) //Call Service
        {

        }//Ask CLR To Create Object From DepartmentServices
    }
}
