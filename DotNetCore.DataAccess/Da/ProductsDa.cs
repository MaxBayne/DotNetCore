using System.Text.Json;
using DotNetCore.DataAccess.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.DataAccess.Da
{
    public class ProductsDa
    {
        private readonly IHostingEnvironment _environment;
        private readonly ErpDbContext _erpDbContext;

        public ProductsDa(IHostingEnvironment environment)
        {
            _environment = environment;
            _erpDbContext = new ErpDbContext();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _erpDbContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>?> GetAllProductsFromJsonFileAsync()
        {
            //Get the path of Json File of Products

            string filePath;

            if (_environment.WebRootPath != null)
            {
                filePath = Path.Combine(_environment.WebRootPath, "data", "products.json");
            }
            else
            {
                filePath = Path.Combine(_environment.ContentRootPath, "data", "products.json");
            }
            

            using var jsonFileReader = File.OpenText(filePath);
            var jsonString = await jsonFileReader.ReadToEndAsync();

            return !string.IsNullOrEmpty(jsonString) ? JsonSerializer.Deserialize<IEnumerable<Product>>(jsonString) : null;

        }

        public async Task<Product> CreateProductAsync(Product newProduct)
        {
            var result = await _erpDbContext.Products.AddAsync(newProduct);
            
            await _erpDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Product?> UpdateProductAsync(Product updatedProduct)
        {
            var currentProduct = await _erpDbContext.Products.SingleOrDefaultAsync(s => s.Id == updatedProduct.Id);

            if (currentProduct != null)
            {
                _erpDbContext.Entry(currentProduct).CurrentValues.SetValues(updatedProduct);

                await _erpDbContext.SaveChangesAsync();
            }

            return currentProduct;
        }

        public async Task<bool> DeleteProductAsync(Product deletedProduct)
        {
            var currentProduct = await _erpDbContext.Products.SingleOrDefaultAsync(s => s.Id == deletedProduct.Id);

            if (currentProduct != null)
            {
                _erpDbContext.Products.Remove(currentProduct);
                await _erpDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var currentProduct = await _erpDbContext.Products.SingleOrDefaultAsync(s => s.Id == id);

            if (currentProduct != null)
            {
                _erpDbContext.Products.Remove(currentProduct);
                await _erpDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
