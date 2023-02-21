using System.ComponentModel.DataAnnotations.Schema;
using DotNetCore.Common.DataModels;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618

namespace DotNetCore.DataAccess.Contexts
{
    public class ErpDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ERP_db;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // guid identities
            foreach (var entity in modelBuilder.Model.GetEntityTypes()
                         .Where(t =>
                             t.ClrType.GetProperties()
                                 .Any(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(DatabaseGeneratedAttribute)))))
            {
                foreach (var property in entity.ClrType.GetProperties()
                             .Where(p => p.PropertyType == typeof(Guid) && p.CustomAttributes.Any(a => a.AttributeType == typeof(DatabaseGeneratedAttribute))))
                {
                    modelBuilder
                        .Entity(entity.ClrType)
                        .Property(property.Name)
                        .HasDefaultValueSql("newsequentialid()");
                }
            }


            // Seeds Some Data

            

        }

       

        #region Tables Sets

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Product> Products { get; set; }

        #endregion
    }
}
