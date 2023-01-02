using Microsoft.EntityFrameworkCore;

namespace DotNetCore.DataAccess.Contexts
{
    public class ErpDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ERP_db;Integrated Security=True;Connect Timeout=30");
            //base.OnConfiguring(optionsBuilder);
        }

        #region Tables Sets

        public DbSet<Product> Products { get; set; }    

        #endregion
    }
}
