using DotNetCore.Common.DataModels;
using DotNetCore.DataAccess.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.BusinessLogic.Services
{

    public interface IDepartmentsService
    {
        Task<Department> GetByIdAsync(Guid id);
        Task<List<Department>> GetAllAsync();
        Task<Department> CreateAsync(Department newDepartment);
        Task<Department> UpdateAsync(Department updatedDepartment);
        Task<bool> DeleteAsync(Department deletedDepartment);
        Task<bool> DeleteByIdAsync(Guid id);
    }

    public class DepartmentsService : IDepartmentsService
    {
        private readonly IHostingEnvironment _environment;
        private ErpDbContext _dbContext;

        public DepartmentsService(IHostingEnvironment environment)
        {
            _environment = environment;
            _dbContext = new ErpDbContext();
        }

        public async Task<Department> GetByIdAsync(Guid id)
        {
            return (await _dbContext.Departments.FirstOrDefaultAsync(c => c.Id == id))!;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _dbContext.Departments.ToListAsync();
        }

        public async Task<Department> CreateAsync(Department newDepartment)
        {
            var result = await _dbContext.Departments.AddAsync(newDepartment);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Department> UpdateAsync(Department updatedDepartment)
        {
            var findDepartment = await _dbContext.Departments.FirstOrDefaultAsync(c=>c.Id== updatedDepartment.Id);

            if (findDepartment != null)
            {
                _dbContext.Entry(findDepartment).CurrentValues.SetValues(updatedDepartment);
                await _dbContext.SaveChangesAsync();
            }
            
            return updatedDepartment;
        }

        public async Task<bool> DeleteAsync(Department deletedDepartment)
        {
            _dbContext.Departments.Remove(deletedDepartment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var findDepartment = await _dbContext.Departments.FirstOrDefaultAsync(c => c.Id == id);

            if (findDepartment != null)
            {
                _dbContext.Departments.Remove(findDepartment);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;

        }

    }
}