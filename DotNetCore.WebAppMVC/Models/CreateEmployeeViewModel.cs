using DotNetCore.Common.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
#pragma warning disable CS8618

namespace DotNetCore.WebAppMVC.Models;

public class CreateEmployeeViewModel
{
    public CreateEmployeeViewModel()
    {
        Employee = new Employee(){DateOfBirth = DateTime.Now};
        Departments = new List<Department>();
    }

    public Employee Employee { get; set; }

    public List<Department> Departments
    {
        set => DepartmentsSelectList = new SelectList(value, nameof(Department.Id), nameof(Department.Name));
    }

    public SelectList DepartmentsSelectList { get; private set; }
}