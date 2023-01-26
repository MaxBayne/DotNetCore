using DotNetCore.Common.DataModels;

namespace DotNetCore.WebAppMVC.Models;

public class EmployeesViewModel
{
    public EmployeesViewModel(IEnumerable<Employee> employees)
    {
        Employees = employees;
    }

    public IEnumerable<Employee> Employees { get; set; }
}