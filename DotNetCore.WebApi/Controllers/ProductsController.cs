using DotNetCore.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet(Name = "GetAllProducts")]
        public async Task<IEnumerable<Product>?> Get()
        {
            return await _productsService.GetAllProductsAsync();
        }
    }
}
