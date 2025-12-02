//using Demo.BLL.DTOs.dtos;
using Demo.BLL.DTOs.DepartmentDtos;
using Demo.BLL.DTOs.EmployeeDtos;
using Demo.BLL.Services.Classes;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeesController(IEmployeeServices _employeeServices, IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var employees=_employeeServices.GetAllEmployees(); //default as no tracking
            return View(employees);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int res = _employeeServices.AddEmployee(dto);
                    if (res > 0) return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to add Employee");
                        return View(dto);
                    }
                }
                catch (Exception ex)
                {
                    // log Exception
                    if (_environment.IsDevelopment())
                    {
                        //1) Development => Log Error in Console And return the same view with the Error Message
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(dto);
                    }
                    else
                    {
                        //2) Production => Log Error in File | DataBase | Table And return the same view with the Error Message
                        //_logger.LogError(ex.Message);
                        return View(dto);
                    }
                }
            }
            else return View(dto);
        }
        #endregion


        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var emp = _employeeServices.GetEmployeeById(id.Value);
            if (emp is null) return NotFound();// 404
            return View(emp);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var emp = _employeeServices.GetEmployeeById(id.Value);
            if (emp is null) return NotFound();// 404
            var dto = new UpdateEmployeeDto()
            {
                Id = emp.Id,
                Name = emp.Name,
                Age = emp.Age,
                Address = emp.Address,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Salary = emp.Salary,
                HiringDate = emp.HiringDate,
                IsActive = emp.IsActive,
                EmployeeType = Enum.Parse<EmployeeType>(emp.EmployeeType),
                Gender = Enum.Parse<Gender>(emp.Gender)
            };
            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, UpdateEmployeeDto dto)
        {
            if(id != dto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(dto);
            try
            {
                var res = _employeeServices.UpdateEmployee(dto);
                if (res > 0) return RedirectToAction(nameof(Index));
                    return View(dto);

            }
            catch (Exception ex)
            {
                // log Exception
                if (_environment.IsDevelopment())
                {
                    //1) Development => Log Error in Console And return the same view with the Error Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(dto);
                }
                else
                {
                    //2) Production => Log Error in File | DataBase | Table And return the same view with the Error Message
                    //_logger.LogError(ex.Message);
                    return View(dto);
                }
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool isDeleted = _employeeServices.DeleteEmployee(id);
                if (isDeleted)
                    return RedirectToAction(nameof(Index));

                else ModelState.AddModelError(string.Empty, "Employee can't be deleted");
                return RedirectToAction(nameof(Delete), new { id });
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
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
