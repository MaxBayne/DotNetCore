using DotNetCore.BusinessLogic.Services;
using DotNetCore.WebApi.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore.WebApi.MSTests
{
    [TestClass]
    public class ProductsControllerTests
    {
        private readonly ILogger<ProductsController> _fakeLogger;
        private readonly IProductsService _fakeProductService;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly List<Product> _productsDummy;

        public ProductsControllerTests()
        {
            //Fake
            _fakeLogger = A.Fake<ILogger<ProductsController>>();
            _fakeProductService = A.Fake<IProductsService>();
            _productsDummy = new List<Product>()
            {
                new Product() { Id = "1", Name = "Product 1" },
                new Product() { Id = "2", Name = "Product 2" },
                new Product() { Id = "3", Name = "Product 3" },
                new Product() { Id = "4", Name = "Product 4" },
                new Product() { Id = "5", Name = "Product 5" }
            };

            //Config
            A.CallTo(() => _fakeProductService.GetAllProductsAsync()).Returns(_productsDummy);
        }

        [TestMethod]
        public async Task Get_Product_BY_Id_Return_Ok()
        {
            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);
            string id = "1";

            //Act
            var actionResult = await controller.GetProductById(id);
            var actualResult = actionResult as OkObjectResult;

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

        }

        [TestMethod]
        public async Task Get_Product_BY_Id_Return_Not_Found()
        {
            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);
            string id = "100";

            //Act
            var actionResult = await controller.GetProductById(id);
            var actualResult = actionResult as NotFoundResult;

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, actualResult.StatusCode);

        }


        [TestMethod]
        public async Task Get_All_Products_Return_Ok()
        {
            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);

            //Act
            var actionResult = await controller.GetAllProducts();
            var actualResult = actionResult as OkObjectResult;

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_All_Products_Return_Not_Found()
        {
            //Fake
            A.CallTo(() => _fakeProductService.GetAllProductsAsync()).Returns(new List<Product>());

            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);

            //Act
            var actionResult = await controller.GetAllProducts();
            var actualResult = actionResult as NotFoundResult;

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, actualResult.StatusCode);
        }


        [TestMethod]
        public void Create_Product_Return_Created()
        {
            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);

            //Act
            var actionResult = controller.CreateProduct(new Product
            {
                Id = "1000",
                Name = "Test Product"

            });
            var actualResult = actionResult as CreatedResult;

            //Assert
            Assert.AreEqual(StatusCodes.Status201Created, actualResult?.StatusCode);
        }

        [TestMethod]
        public void Create_Product_Return_BadRequest()
        {
            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);

            //Act
            var actionResult = controller.CreateProduct(new Product
            {
                Name = "Test Product"
            });
            var actualResult = actionResult as BadRequestObjectResult;

            //Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, actualResult?.StatusCode);
        }
    }
}