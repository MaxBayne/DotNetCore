namespace DotNetCore.WebAppMVC.Models
{
    public class ProductsViewModel
    {
        public ProductsViewModel(IEnumerable<Product> products)
        {
            Products = products;
        }

        public IEnumerable<Product> Products { get; set; }
    }
}
