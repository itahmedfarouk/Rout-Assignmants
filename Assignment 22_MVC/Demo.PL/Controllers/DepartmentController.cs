using Demo.BLL.DTOs;
using Demo.BLL.DTOs.DepartmentDtos;
using Demo.BLL.Services.Classes;
using Demo.BLL.Services.Interfaces;
using Demo.PL.ViewModels.DepartmentViewModels;
using Microsoft.AspNetCore.Mvc;
namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _departmentService;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentServices departmentService,
                                    ILogger<HomeController> logger,
                                    IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            this._logger = logger;
            this._environment = environment;
        }
        // Get BaseUrl/Departments/Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddDepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int res = _departmentService.AddDepartment(departmentDto);
                    if (res > 0) return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to add Department");
                        return View(departmentDto);
                    }
                }
                catch (Exception ex)
                {
                    // log Exception
                    if (_environment.IsDevelopment())
                    {
                        //1) Development => Log Error in Console And return the same view with the Error Message
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(departmentDto);
                    }
                    else
                    {
                        //2) Production => Log Error in File | DataBase | Table And return the same view with the Error Message
                        //_logger.LogError(ex.Message);
                        return View(departmentDto);
                    }
                }
            }
            else return View(departmentDto);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();// 404
            return View(department);
        }

        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            //Detailed Department Dto

            if (department is null) return NotFound();
            var deptViewModel = new DepartmentEditViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                DateOfCreation = department.DateOfCreation,
                Description = department.Description
            };
            return View(deptViewModel);//wairt for department Edit view model

        }
        [HttpPost]
        public IActionResult Edit(int id, DepartmentEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var updateDeptDto = new UpdateDepartmentDto() { 
                    Id=id,
                    Name = viewModel.Name,
                    Code = viewModel.Code,
                    DateOfCreation = viewModel.DateOfCreation,
                    Description = viewModel.Description
               };
            var res= _departmentService.UpdateDepartment(updateDeptDto);
                if(res>0) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to Update Department");
                    return View(viewModel);
                }

            }
            catch(Exception ex)
            {
                // log Exception
                if (_environment.IsDevelopment())
                {
                    //1) Development => Log Error in Console And return the same view with the Error Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(viewModel);
                }
                else
                {
                    //2) Production => Log Error in File | DataBase | Table And return the same view with the Error Message
                    //_logger.LogError(ex.Message);
                    return View(viewModel);
                }
            }
        }



        #endregion

        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var dept = _departmentService.GetDepartmentById(id.Value);
        //    if (dept is null) return NotFound();
        //    return View(dept);
        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool isDeleted = _departmentService.DeleteDepartment(id);
                if (isDeleted)
                    return RedirectToAction(nameof(Index));

                else ModelState.AddModelError(string.Empty, "Department can't be deleted");
                return RedirectToAction(nameof(Delete), new { id });
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    var dept = _departmentService.GetDepartmentById(id);
                    return View(dept);
                }
                else
                {
                    //_logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion

    }
}
