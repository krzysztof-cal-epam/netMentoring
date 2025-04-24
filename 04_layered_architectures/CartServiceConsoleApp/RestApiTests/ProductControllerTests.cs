using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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
        public async Task ListProductsByCategory_ReturnsOK()
        {
            // Arrange
            var productList = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Product A", Price = 100.0m, Amount = 10, CategoryId = 1 },
                new ProductDto { Id = 2, Name = "Product B", Price = 200.0m, Amount = 5, CategoryId = 2 }
            };
            _mockProductService
                .Setup(service => service.ListAsync(It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(productList);

            // Act
            var res = await _productController.ListProductsByCategory(1, 1, 10) as OkObjectResult;

            // Assert
            var returnedProducts = res?.Value as List<ProductDto>;
            Assert.IsType<OkObjectResult>(res);
            Assert.NotNull(returnedProducts);
            Assert.Equal(2, returnedProducts.Count);
            Assert.Equal("Product A", returnedProducts.FirstOrDefault()?.Name);
            _mockProductService.Verify(x => x.ListAsync(It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
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

        [Fact]
        public async Task Update_UpdatedProduct_ReturnsNoContent()
        {
            // Arrange
            var expectedProduct = new ProductDto { Id = 1, Name = "Product A", Price = 100.0m, Amount = 10, CategoryId = 1 };
            _mockProductService
                .Setup(service => service.UpdateAsync(It.IsAny<ProductDto>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _productController.Update(1, expectedProduct) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
            _mockProductService.Verify(service => service.UpdateAsync(It.IsAny<ProductDto>()), Times.Once);
        }

        [Fact]
        public async Task Delete_DeletesProduct_ReturnsNoContent()
        {
            // Arrange
            var givenProductIdToDelete = 1;
            _mockProductService
                .Setup(service => service.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _productController.Delete(givenProductIdToDelete) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
            _mockProductService.Verify(service => service.DeleteAsync(It.Is<int>(x => x == givenProductIdToDelete)), Times.Once);
        }

        [Fact]
        public async Task GetById_ReturnsOKWithLinks()
        {
            // Arrange
            var expectedProduct = new ProductDto
            {
                Id = 1, 
                Name = "Product A",
                Description = "Product A description",
                Image = new Uri("http://ecommerceapp.com/images/producta.jpg"),
                Price = 100.0m,
                Amount = 10,
                CategoryId = 1 
            };
            _mockProductService
                .Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedProduct);

            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(helper => helper.Action(It.IsAny<UrlActionContext>()))
                .Returns((UrlActionContext context) =>
                {
                    var id = context.Values.GetType().GetProperty("id")?.GetValue(context.Values, null);

                    return $"/api/products/{id}";
                });
            _productController.Url = mockUrlHelper.Object;

            // Act
            var res = await _productController.GetById(1) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(res);
            var product = res?.Value as ProductWithLinksDto;
            Assert.NotNull(product);
            Assert.Equal(expectedProduct.Name, product?.Name);
            
            Assert.NotNull(product.Links);
            Assert.Equal($"/api/products/1", product.Links.Self.Href);
        }
    }
}