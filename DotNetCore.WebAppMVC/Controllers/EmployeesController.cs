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
            if (ModelState.IsValid)
            {
                model.Employee.Id = Guid.NewGuid();

                Repository.AddEmployee(model.Employee);

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion
    }
}
