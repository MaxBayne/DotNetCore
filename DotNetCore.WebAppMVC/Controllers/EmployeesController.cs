using DotNetCore.BusinessLogic.Services;
using DotNetCore.Common.DataModels;
using DotNetCore.WebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;



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
            ModelState.Remove("Employee.Department");

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

        #region Read

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _employeesService.GetAllAsync();

            var model = new EmployeesViewModel(data!);

            return View(model);
        }


        #endregion

        #region Update

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
        public async Task<IActionResult> EditEmployee(Guid id, EditEmployeeViewModel model)
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

        #region Sort

        [HttpGet]
        public async Task<IActionResult> Sort(string field, Extensions.SortDirection direction)
        {
            ViewBag.SortDirection = direction == Extensions.SortDirection.Ascending ? Extensions.SortDirection.Descending : Extensions.SortDirection.Ascending;

            var data = await _employeesService.GetAllAsync();

            var dataSorted = data.Sort(field, direction);

            var model = new EmployeesViewModel(dataSorted);

            return View("Index", model);
        }


        #endregion

        #region Search

        [HttpGet]
        public async Task<IActionResult> Search(string by,string field = "Name")
        {
            //ViewBag.SortDirection = direction == Extensions.SortDirection.Ascending ? Extensions.SortDirection.Descending : Extensions.SortDirection.Ascending;

            var data = await _employeesService.GetAllAsync();


            var dataSearched = data.Search(field, by);

            var model = new EmployeesViewModel(dataSearched);

            return View("Index", model);
        }

        #endregion
    }

    public static class Extensions
    {
        public enum SortDirection
        {
            Ascending,
            Descending

        }

        public static List<Employee> Sort(this IEnumerable<Employee>? employees, string field, SortDirection direction)
        {
            var propertyInfo = typeof(Employee).GetProperty(field);

            return direction == SortDirection.Descending ? employees!.OrderByDescending(employee => propertyInfo?.GetValue(employee, null)).ToList() : employees!.OrderBy((employee) => propertyInfo?.GetValue(employee, null)).ToList();
        }
        public static List<Employee> Search(this IEnumerable<Employee>? employees, string field, string by)
        {
            var propertyInfo = typeof(Employee).GetProperty(field);

            if (string.IsNullOrEmpty(by))
            {
                return employees!.ToList();
            }
            else
            {
                return employees!.Where(employee => (string)propertyInfo?.GetValue(employee, null)! == by).ToList();
            }
            
        }
    }
}
