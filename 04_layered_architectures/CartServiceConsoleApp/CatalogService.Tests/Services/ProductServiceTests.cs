﻿using CatalogService.Application.Dto;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using Moq;

namespace CatalogService.Tests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetById_ShouldReturnCorrectDto()
        {
            // Arrange
            var sampleProduct = new Product
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

            var mockProductRepository = new Mock<IRepository<Product>>();
            mockProductRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(sampleProduct);

            var productService = new ProductService(mockProductRepository.Object, new Mock<IRepository<Category>>().Object);

            // Act
            var result = await productService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductDto>(result);
            Assert.Equal(sampleProduct.Id, result.Id);
            Assert.Equal(sampleProduct.Name, result.Name);
            Assert.Equal(sampleProduct.Price, result.Price);
            Assert.Equal(sampleProduct.CategoryId, result.CategoryId);
        }
    }
}
