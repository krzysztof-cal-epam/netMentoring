using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestApi.Controllers;

namespace RestApiTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _productController;
        public ProductControllerTests()
        {
            _mockProductService = new Mock<IProductService>();

            _productController = new ProductController(_mockProductService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOK()
        {
            // Arrange
            var productList = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Product A", Price = 100.0m, Amount = 10, CategoryId = 1 },
                new ProductDto { Id = 2, Name = "Product B", Price = 200.0m, Amount = 5, CategoryId = 2 }
            };
            _mockProductService
                .Setup(service => service.GetProductsAsync(It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(productList);

            // Act
            var res = await _productController.GetAll(1, 1, 10) as OkObjectResult;

            // Assert
            var returnedProducts = res?.Value as List<ProductDto>;
            Assert.IsType<OkObjectResult>(res);
            Assert.NotNull(returnedProducts);
            Assert.Equal(2, returnedProducts.Count);
            Assert.Equal("Product A", returnedProducts.FirstOrDefault()?.Name);
        }

        [Fact]
        public async Task Add_ValidProduct_ReturnsCreated()
        {
            // Arrange
            var expectedProduct = new ProductDto { Id = 1, Name = "Product A", Price = 100.0m, Amount = 10, CategoryId = 1 };
            _mockProductService
                .Setup(service => service.AddAsync(It.IsAny<ProductDto>()))
                .ReturnsAsync(expectedProduct);

            // Act
            var result = await _productController.Add(expectedProduct) as CreatedAtActionResult;

            // Assert
            var product = result?.Value as ProductDto;
            Assert.NotNull(result);
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(product);
            Assert.Equal(expectedProduct.Id, product.Id);
            Assert.Equal(expectedProduct.Name, product.Name);
            _mockProductService.Verify(service => service.AddAsync(It.IsAny<ProductDto>()), Times.Once);
        }

        //todo Update

        //todo Delete
    }
}