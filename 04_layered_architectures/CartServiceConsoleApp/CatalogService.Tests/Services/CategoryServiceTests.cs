using CatalogService.Application.Dto;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using Moq;

namespace CatalogService.Tests.Services
{
    public class CategoryServiceTests
    {
        [Fact]
        public async Task ListAsync_ShouldReturnMappedCategoryDto()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics", ImageUrl = new Uri("https://example.com/electronics.jpg"), ParentCategoryId = null },
                new Category { Id = 2, Name = "Laptops", ImageUrl = null, ParentCategoryId = 1 }
            };
            var mockCategoryRepository = new Mock<IRepository<Category>>();
            mockCategoryRepository
                .Setup(repo => repo.ListAsync())
                .ReturnsAsync(categories);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            // Act
            var result = await categoryService.ListAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categories.Count, result.Count());
            Assert.IsType<CategoryDto>(result.ElementAt(0));
            Assert.IsType<CategoryDto>(result.ElementAt(1));
            Assert.Equal("Electronics", result.ElementAt(0).Name);
            Assert.Equal("Laptops", result.ElementAt(1).Name);
            Assert.Equal(1, result.ElementAt(1).ParentCategoryId);
        }
    }
}
