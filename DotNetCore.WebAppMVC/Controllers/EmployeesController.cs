using DotNetCore.WebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;


namespace DotNetCore.WebAppMVC.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            var data = Repository.GetEmployees;
            var model = new EmployeesViewModel(data);

            return View(model);
        }


        #region Create

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEmployee(CreateEmployeeViewModel model)
        {
            if (string.IsNullOrEmpty(model.Employee.Department))
            {
                ModelState.AddModelError("Department", "Department is Required Man");

                return View(model);
            }

            model.Employee.Id=Guid.NewGuid();

            Repository.AddEmployee(model.Employee);

            return RedirectToAction("Index");

        }

        #endregion
    }
}
