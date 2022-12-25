using DotNetCore.DataAccess.Da;
using Microsoft.AspNetCore.Hosting;

namespace DotNetCore.BusinessLogic.Services
{

    public interface IProductsService
    {
        Task<IEnumerable<Product>?> GetAllProductsAsync();
    }

    public class ProductsService : IProductsService
    {
        private readonly IHostingEnvironment _environment;

        public ProductsService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<IEnumerable<Product>?> GetAllProductsAsync()
        {
            var productsDa = new ProductsDa(_environment);

            return await productsDa.GetAllProductsAsync();
        }

    }
}