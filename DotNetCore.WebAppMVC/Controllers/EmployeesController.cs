using DotNetCore.BusinessLogic.Services;
using DotNetCore.Common.DataModels;
using DotNetCore.WebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace DotNetCore.WebAppMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employeesService;
        private readonly IDepartmentsService _departmentsService;

        public EmployeesController(IEmployeesService employeesService, IDepartmentsService departmentsService)
        {
            _employeesService = employeesService;
            _departmentsService = departmentsService;   
        }

        #region Read

        public async Task<IActionResult> Index()
        {
            var data = await _employeesService.GetAllAsync();
            var model = new EmployeesViewModel(data);

            return View(model);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> CreateEmployee()
        {
            var model = new CreateEmployeeViewModel()
            {
                Departments = await _departmentsService.GetAllAsync(),
                Employee = new Employee()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Employee.Id = Guid.NewGuid();

                await _employeesService.CreateAsync(model.Employee);

                return RedirectToAction("Index");
            }

            model.Departments = await _departmentsService.GetAllAsync();

            return View(model);
        }

        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> EditEmployee(Guid id)
        {
            var employeeToEdit = await _employeesService.GetByIdAsync(id);

            if (employeeToEdit != null)
            {
                var model = new EditEmployeeViewModel()
                {
                    Employee = employeeToEdit,
                    Departments = await _departmentsService.GetAllAsync()
                };

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(Guid id,EditEmployeeViewModel model)
        {
            ModelState.Remove("Employee.Department");

            if (ModelState.IsValid)
            {
                await _employeesService.UpdateAsync(model.Employee);

                return RedirectToAction("Index");
            }

            model.Departments = await _departmentsService.GetAllAsync();

            return View(model);
        }

        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            if (ModelState.IsValid)
            {
                await _employeesService.DeleteByIdAsync(id);

                return RedirectToAction("Index");
            }

            return View("CreateEmployee");
        }

        #endregion
    }
}
