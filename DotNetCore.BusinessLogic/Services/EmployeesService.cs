using DotNetCore.Common.DataModels;
using DotNetCore.DataAccess.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.BusinessLogic.Services
{

    public interface IEmployeesService
    {
        Task<Employee?> GetByIdAsync(Guid id);
        Task<IEnumerable<Employee>?> GetAllAsync();
        Task<Employee> CreateAsync(Employee newEmployee);
        Task<Employee?> UpdateAsync(Employee updatedEmployee);
        Task<bool> DeleteAsync(Employee deletedEmployee);
        Task<bool> DeleteByIdAsync(Guid id);
    }

    public class EmployeesService : IEmployeesService
    {
        private readonly IHostingEnvironment _environment;
        private ErpDbContext _dbContext;

        public EmployeesService(IHostingEnvironment environment)
        {
            _environment = environment;
            _dbContext = new ErpDbContext();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Employee>?> GetAllAsync()
        {
            var allEmployees =  await _dbContext.Employees.Include(c=>c.Department).ToListAsync();

            //var allEmployees = await (from employee in _dbContext.Employees
            //    join department in _dbContext.Departments on employee.DepartmentId equals department.Id
            //    select new Employee()
            //    {
            //        Id = employee.Id,
            //        Name = employee.Name,
            //        DateOfBirth = employee.DateOfBirth,
            //        Email = employee.Email,
            //        Phone = employee.Phone,
            //        IsActive = employee.IsActive,
            //        DepartmentId = employee.DepartmentId,
            //        Department = new Department()
            //        {
            //            Id = employee.DepartmentId,
            //            Name = department.Name
            //        }

            //    }).ToListAsync();

            return allEmployees;
        }

        public async Task<Employee> CreateAsync(Employee newEmployee)
        {
            var result = _dbContext.Employees.Add(newEmployee);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee?> UpdateAsync(Employee updatedEmployee)
        {
            var findEmployee = await _dbContext.Employees.FirstOrDefaultAsync(c=>c.Id== updatedEmployee.Id);

            _dbContext.Entry(findEmployee).CurrentValues.SetValues(updatedEmployee);

            await _dbContext.SaveChangesAsync();

            return updatedEmployee;
        }

        public async Task<bool> DeleteAsync(Employee deletedEmployee)
        {
            _dbContext.Employees.Remove(deletedEmployee);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var findEmployee = await _dbContext.Employees.FirstOrDefaultAsync(c => c.Id == id);

            _dbContext.Employees.Remove(findEmployee);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}