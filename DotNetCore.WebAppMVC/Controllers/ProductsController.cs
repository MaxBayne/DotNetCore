using DotNetCore.BusinessLogic.Services;
using DotNetCore.WebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.WebAppMVC.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _productsService;

        // GET: ProductsController
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<ActionResult> Index()
        {
            var results = await _productsService.GetAllProductsAsync();

            return View(new ProductsViewModel(results!));
        }
        
    }
}
