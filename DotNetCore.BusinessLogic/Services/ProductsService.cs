using DotNetCore.DataAccess.Da;
using Microsoft.AspNetCore.Hosting;

namespace DotNetCore.BusinessLogic.Services
{

    public interface IProductsService
    {
        Task<IEnumerable<Product>?> GetAllProductsAsync();
        Task<Product> CreateProductAsync(Product newProduct);
        Task<Product?> UpdateProductAsync(Product updatedProduct);
        Task<bool> DeleteProductAsync(Product deletedProduct);
        Task<bool> DeleteProductAsync(string id);
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

        public async Task<Product> CreateProductAsync(Product newProduct)
        {
            var productsDa = new ProductsDa(_environment);

            return await productsDa.CreateProductAsync(newProduct);
        }

        public async Task<Product?> UpdateProductAsync(Product updatedProduct)
        {
            var productsDa = new ProductsDa(_environment);

            return await productsDa.UpdateProductAsync(updatedProduct);
        }

        public async Task<bool> DeleteProductAsync(Product deletedProduct)
        {
            var productsDa = new ProductsDa(_environment);

            return await productsDa.DeleteProductAsync(deletedProduct);
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var productsDa = new ProductsDa(_environment);

            return await productsDa.DeleteProductAsync(id);
        }

    }
}