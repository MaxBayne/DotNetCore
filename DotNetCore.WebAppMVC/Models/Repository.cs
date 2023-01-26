using DotNetCore.Common.DataModels;

namespace DotNetCore.WebAppMVC.Models
{
    public class Repository
    {
        private static List<Employee> _employees=new List<Employee>();

        public static List<Employee> GetEmployees => _employees;

        public static void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        public static void RemoveEmployee(Employee employee)
        {
            _employees.Remove(employee);
        }
    
}
}
