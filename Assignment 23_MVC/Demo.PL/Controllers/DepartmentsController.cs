using Demo.BL2.DTOS;
using Demo.BL2.DTOS.Department;
using Demo.BL2.Services.Interfaces;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
	public class DepartmentsController(IDepartmentService _departmentService , ILogger<DepartmentsController> _logger ,IWebHostEnvironment _environment) : Controller
	{

		public IActionResult Index()
		{
			var departments = _departmentService.GetAll();
			// example for viewdata and viewbag
			ViewData["msg1"] = "hello from viewdata";
			ViewBag.msg2 = "hello from viewbag";
			return View(departments);
		}
		#region Create
		[HttpGet]
		public IActionResult Create()
		{
				return View();
		}
		[HttpPost]
		public IActionResult Create(DepartmentViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var createdDepartmentDto = new CreatedDepartmentDto
					{
						Name = viewModel.Name,
						Code = viewModel.Code,
						Description = viewModel.Description,
						DateOfCreation = viewModel.DateOfCreation
					};
					
					int res = _departmentService.Add(createdDepartmentDto);
					string message = res > 0 ? "Department Added Successfully" : "Faild to Add Department";
					TempData["Message"] = message;
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					if (_environment.IsDevelopment())
					{
						_logger.LogError(ex, "Error occurred while adding a department.");
						ModelState.AddModelError(string.Empty, ex.Message);
						return View(viewModel);
					}
					else
					{
						_logger.LogError(ex, "Error occurred while adding a department.");
						ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again later.");
						return View(viewModel);

					}
				}
			}
			else
			{
				return View(viewModel);

			}
		}
		#endregion
		#region Details
		[HttpGet]
		public IActionResult Details(int? id)
		{
			if (id == null) return BadRequest();
			var department = _departmentService.GetById(id);
			if (department == null) return NotFound();
			return View(department);
		}
		#endregion
		#region Edits
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (id == null) return BadRequest();
			var department = _departmentService.GetById(id);
			if (department == null) return NotFound();
			var ViewModel = new DepartmentViewModel
			{
				Name = department.Name,
				Code = department.Code,
				Description = department.Description,
				DateOfCreation = department.DateOfCreation
			};
			return View(ViewModel);
		}
		[HttpPost]
		public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentEditViewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var updatedDepartmentDto = new UpdatedDepartmentDto
					{
						Id = id,
						Name = departmentEditViewModel.Name,
						Code = departmentEditViewModel.Code,
						Description = departmentEditViewModel.Description,
						DateOfCreation = departmentEditViewModel.DateOfCreation
					};
					int res = _departmentService.Update(updatedDepartmentDto);
					if (res > 0) return RedirectToAction("Index");
					else
					{
						ModelState.AddModelError(String.Empty, "Faild to Update Department");
						return View(departmentEditViewModel);
					}
				}
				catch (Exception ex)
				{
					if (_environment.IsDevelopment())
					{
						_logger.LogError(ex, "Error occurred while updating a department.");
						ModelState.AddModelError(string.Empty, ex.Message);
						return View(departmentEditViewModel);
					}
					else
					{
						_logger.LogError(ex, "Error occurred while updating a department.");
						ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again later.");
						return View(departmentEditViewModel);
					}
				}
			}
			else
			{
				return View(departmentEditViewModel);
			}
		}
		#endregion
		#region Delete

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (!id.HasValue) return BadRequest();
			var department = _departmentService.GetById(id);
			if (department == null) return NotFound();
			return View(department);
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			try
			{
				bool res = _departmentService.Remove(id);
				if (res) return RedirectToAction("Index");
				else
				{
					ModelState.AddModelError(String.Empty, "Faild to Delete Department");
					var department = _departmentService.GetById(id);
					return View(department);
				}
			}
			catch (Exception ex)
			{
				if (_environment.IsDevelopment())
				{
					_logger.LogError(ex, "Error occurred while deleting a department.");
					ModelState.AddModelError(string.Empty, ex.Message);
					var department = _departmentService.GetById(id);
					return View(department);
				}
				else
				{
					_logger.LogError(ex, "Error occurred while deleting a department.");
					ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again later.");
					var department = _departmentService.GetById(id);
					return View(department);
				}
			}
		}
		#endregion
	}
}
