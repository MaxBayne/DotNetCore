using System.Net.Mime;
using DotNetCore.BusinessLogic.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1.0/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly ILogger<ProductsController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productsService"></param>
        public ProductsController(ILogger<ProductsController> logger,IProductsService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }

        #region Binding Source

        //BindingSource
        //[FromQuery] = from request query string
        //[FromBody] = from request body
        //[FromHeader] = from request header
        //[FromForm] = from Form Data
        //[FromRoute] = from request routing
        //[FromServices] = from request service

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Post: api/products
        /// Action[Create] , Method = [HttPPost] , OnSuccess = [Created(201)] , OnFailure = [BadRequest(400)]
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateProduct([FromBody]Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.Id))
                {
                    throw new Exception();
                }

                _logger.LogInformation("created new product");
                //return await _productsService.GetAllProductsAsync();
                return Created("",product);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
           
        }

        /// <summary>
        /// Get: api/products
        /// Action [Get] , Method = [HttPGet] , OnSuccess = [Ok(200)] , OnFailure = [NotFound(404)]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productsService.GetAllProductsAsync();

                if (products!.Any())
                {
                    return Ok(products);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
           
        }

        /// <summary>
        /// Get: api/products/{id}
        /// Action [Get] , Method = [HttPGet] , OnSuccess = [Ok(200)] , OnFailure = [NotFound(404)]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductById([FromRoute] string id)
        {
            try
            {
                var products = ((await _productsService.GetAllProductsAsync())!).ToList();

                var productExist = products.FirstOrDefault(c => c.Id == id);

                if (productExist != null)
                {
                    return Ok(productExist);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
           
        }

        /// <summary>
        /// Put: api/products/{id}
        /// Action [Update] , Method = [HttpPut] , OnSuccess = [NoContent(204)] , OnFailure = [NotFound(404)]
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromRoute]string id,[FromBody]Product product)
        {
            try
            {
                await _productsService.UpdateProductAsync(product);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }

        /// <summary>
        /// Patch: api/products/{id}
        /// Action [Update Some Entity] , Method = [HttpPatch] , OnSuccess = [NoContent(204)] , OnFailure = [NotFound(404)]
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedProduct"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct([FromRoute]string id , JsonPatchDocument<Product> updatedProduct)
        {
            try
            {
                var productsList = new List<Product>
                {
                    new Product {Id="1",Name = "Product 1"},
                    new Product {Id="2",Name = "Product 2"},
                    new Product {Id="3",Name = "Product 3"},
                    new Product {Id="4",Name = "Product 4"},
                    new Product {Id="5",Name = "Product 5"}
                };

                var productNeededForUpdate = productsList.FirstOrDefault(c => c.Id == id);

                if (productNeededForUpdate == null)
                {
                    //On Fail
                    return NotFound();
                }

                //update product
                updatedProduct.ApplyTo(productNeededForUpdate);

                //On Success
                //return NoContent();
                return Ok(productNeededForUpdate);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            

        }

        /// <summary>
        /// Delete: api/products/{id:string}
        /// Action [Delete] , Method = [HttpDelete] , OnSuccess = [NoContent(204)] , OnFailure = [BadRequest(400)]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct([FromRoute] string id)
        {
            try
            {
                await _productsService.DeleteProductAsync(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion
    }
}
