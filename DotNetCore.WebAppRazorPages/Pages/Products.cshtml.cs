using DotNetCore.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetCore.WebAppRazorPages.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IProductsService _jsonFileProductsService;

        public IEnumerable<Product>? Products { get; private set; }


        public ProductsModel(IProductsService jsonFileProductsService)
        {
            //IJsonFileProductsService Will be Injected using Built in Dependency Injection config inside Program.cs by set Services
            _jsonFileProductsService = jsonFileProductsService;
            Products = new List<Product>();
        }

        public async Task OnGet()
        {
            Products = (await _jsonFileProductsService.GetAllProductsAsync())!;
        }
    }
}
