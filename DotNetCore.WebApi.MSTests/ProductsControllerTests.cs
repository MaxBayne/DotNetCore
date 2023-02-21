using DotNetCore.BusinessLogic.Services;
using DotNetCore.WebApi.Controllers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore.WebApi.MSTests
{
    [TestClass]
    public class ProductsControllerTests
    {
        static ILogger<ProductsController>? _fakeLogger;
        static IProductsService? _fakeProductService;
        static List<Product>? _productsDummy;


        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            //Fake
            _fakeLogger = A.Fake<ILogger<ProductsController>>();
            _fakeProductService = A.Fake<IProductsService>();
            _productsDummy = new List<Product>()
            {
                new Product {Id=new Guid("8763FA21-9884-4F2A-BFBD-697D35B1454A"),Name = "Product 1"},
                new Product {Id=new Guid("EC97F074-D925-4148-BF8F-CF5F851B3975"),Name = "Product 2"},
                new Product {Id=new Guid("B56691BB-7B54-4C85-B7B5-998D96B988C3"),Name = "Product 3"},
                new Product {Id=new Guid("2985B68C-3BA2-42EA-9B3A-C93BE774E740"),Name = "Product 4"},
                new Product {Id=new Guid("255A6C33-8774-4708-878E-D516837ED5BC"),Name = "Product 5"}
            };

            //Config
            A.CallTo(() => _fakeProductService.GetAllProductsAsync()).Returns(_productsDummy);

            context.WriteLine("InitializeClass");
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            
        }


        [TestCategory("Create")]
        [TestMethod]
        public async void CreateProduct_Valid_Data_Return_Created()
        {
            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);

            //Act
            var actionResult = await controller.CreateProduct(new Product
            {
                Id = new Guid("155A6C33-8774-4708-878E-D516837ED5BC"),
                Name = "Test Product"

            });
            var actualResult = actionResult as CreatedResult;

            //Assert
            //Assert.AreEqual(StatusCodes.Status201Created, actualResult?.StatusCode);
            //Assert.IsInstanceOfType<CreatedResult>(actualResult);

            //using FluentAssertions ====================
            actualResult.Should().BeOfType<CreatedResult>();

        }

        [TestCategory("Create")]
        [TestMethod]
        public async void CreateProduct_Without_Product_Return_BadRequest()
        {
            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);

            //Act
            var actionResult = await controller.CreateProduct(null);
            var actualResult = actionResult as BadRequestResult;

            //Assert
            //Assert.AreEqual(StatusCodes.Status400BadRequest, actualResult?.StatusCode);
            Assert.IsInstanceOfType<BadRequestResult>(actualResult);

            //using FluentAssertions ====================
            //actualResult.Should().BeOfType<BadRequestObjectResult>();
        }

        [TestCategory("Create")]
        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public void CreateProduct_Without_Id_Throw_MissingFieldException()
        {
            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);

            //Act
            var actionResult = controller.CreateProduct(new Product
            {
                Name = "Test Product"
            });

            //Assert
            //ExpectedException of type MissingFieldException
        }


        [TestCategory("Get")]
        [TestMethod]
        public async Task GetAllProducts_Return_Ok_With_Products_List()
        {
            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);

            //Act
            var actionResult = await controller.GetAllProducts();
            var actualResult = actionResult as OkObjectResult;

            //Assert
            //Assert.IsNotNull(actualResult);
            //Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsInstanceOfType<OkObjectResult>(actualResult);

            //using FluentAssertions ====================
            actualResult.Should().BeOfType<OkObjectResult>();
        }

        [TestCategory("Get")]
        [TestMethod]
        public async Task GetAllProducts_With_Empty_Data_Return_Not_Found()
        {
            //Fake
            A.CallTo(() => _fakeProductService.GetAllProductsAsync()).Returns(new List<Product>());

            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);

            //Act
            var actionResult = await controller.GetAllProducts();
            var actualResult = actionResult as NotFoundResult;

            //Assert
            //Assert.IsNotNull(actualResult);
            //Assert.AreEqual(StatusCodes.Status404NotFound, actualResult.StatusCode);
            //Assert.IsInstanceOfType<NotFoundResult>(actualResult);

            //using FluentAssertions ====================
            actualResult.Should().BeOfType<NotFoundResult>();
        }


        [TestCategory("Get")]
        [TestMethod]
        public async Task GetProductById_With_Exist_Id_Return_Ok_With_Product_Data()
        {
            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);
            Guid id = new Guid("255A6C33-8774-4708-878E-D516837ED5BC");

            //Act
            var actionResult = await controller.GetProductById(id);
            var actualResult = actionResult as OkObjectResult;

            //Assert
            //Assert.IsNotNull(actualResult);
            //Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            //Assert.IsInstanceOfType<OkObjectResult>(actualResult);

            //using FluentAssertions ====================
            actualResult.Should().BeOfType<OkObjectResult>();

        }

        [TestCategory("Get")]
        [TestMethod]
        public async Task GetProductById_Not_Exist_Id_Return_Not_Found()
        {
            //Arrange
            var controller = new ProductsController(_fakeLogger, _fakeProductService);
            Guid id = new Guid("255A6C33-8774-4708-878E-D516837ED5BC");

            //Act
            var actionResult = await controller.GetProductById(id);
            var actualResult = actionResult as NotFoundResult;

            //Assert
            //Assert.IsNotNull(actualResult);
            //Assert.AreEqual(StatusCodes.Status404NotFound, actualResult.StatusCode);
            //Assert.IsInstanceOfType<NotFoundResult>(actualResult);

            //using FluentAssertions ====================
            actualResult.Should().BeOfType<NotFoundResult>();

        }
        
        
    }
}