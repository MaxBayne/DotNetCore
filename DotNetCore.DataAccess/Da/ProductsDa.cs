using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace DotNetCore.DataAccess.Da
{
    public class ProductsDa
    {
        private readonly IHostingEnvironment _environment;

        public ProductsDa(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<IEnumerable<Product>?> GetAllProductsAsync()
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
    }
}
