using CatalogService.Application.Dto;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using Moq;

namespace CatalogService.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Product _sampleProduct = new Product()
        {
            Id = 1,
            Name = "Laptop ABC",
            Description = "Description of the laptop ABC",
            Price = 2999.99m,
            Amount = 5,
            CategoryId = 2,
            Category = new Category
            {
                Id = 2,
                Name = "Electronics"
            }
        };

        [Fact]
        public async Task GetById_ShouldReturnCorrectDto()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(_sampleProduct);

            var productService = new ProductService(mockProductRepository.Object, new Mock<IRepository<Category>>().Object);

            // Act
            var result = await productService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductDto>(result);
            Assert.Equal(_sampleProduct.Id, result.Id);
            Assert.Equal(_sampleProduct.Name, result.Name);
            Assert.Equal(_sampleProduct.Price, result.Price);
            Assert.Equal(_sampleProduct.CategoryId, result.CategoryId);
        }

        [Fact]
        public async Task ProductService_GetProductsAsync_ShouldReturnData()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(repo => repo.GetProductsAsync(It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new[] { _sampleProduct });

            var productService = new ProductService(mockProductRepository.Object, new Mock<IRepository<Category>>().Object);

            // Act
            var result = await productService.GetProductsAsync(2, 1, 1);

            // Assert
            Assert.NotEmpty(result);
            Assert.True(result.Any());
            Assert.Equal(_sampleProduct.Name, result.ElementAt(0).Name);
            mockProductRepository.Verify(x => x.GetProductsAsync(It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}
