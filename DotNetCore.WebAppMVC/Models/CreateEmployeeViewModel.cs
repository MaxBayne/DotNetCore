using DotNetCore.Common.DataModels;

namespace DotNetCore.WebAppMVC.Models;

public class CreateEmployeeViewModel
{
    public CreateEmployeeViewModel()
    {
        Employee = new Employee();
    }

    public Employee Employee { get; set; }
}