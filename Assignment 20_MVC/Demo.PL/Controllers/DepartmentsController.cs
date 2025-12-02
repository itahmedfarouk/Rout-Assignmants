using Demo.BLL.Services;
using Microsoft.AspNetCore.Mvc;
namespace Demo.PL.Controllers
{
    public class DepartmentsController(IDepartmentServices _departmentService) : Controller
    {
        // Get BaseUrl/Departments/Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

    }
}
