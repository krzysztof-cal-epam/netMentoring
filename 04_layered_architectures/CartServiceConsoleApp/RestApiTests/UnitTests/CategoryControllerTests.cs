using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestApi.Controllers.V1;

namespace RestApiTests.UnitTests
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly CategoryController _categoryController;
        public CategoryControllerTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();

            _categoryController = new CategoryController(_mockCategoryService.Object);
        }

        [Fact]
        public async Task ListAll_ReturnsOK()
        {
            // Arrange
            var categoryList = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "Electronics", ImageUrl = new Uri("http://ecommerceapp.com/images/electronics.jpg")},
                new CategoryDto { Id = 2, Name = "Laptops", ImageUrl = new Uri("http://ecommerceapp.com/images/laptops.jpg"), ParentCategoryId = 1}
            };
            _mockCategoryService
                .Setup(service => service.ListAsync())
                .ReturnsAsync(categoryList);

            // Act
            var res = await _categoryController.ListAll() as OkObjectResult;

            // Assert
            var returnedProducts = res?.Value as List<CategoryDto>;
            Assert.IsType<OkObjectResult>(res);
            Assert.NotNull(returnedProducts);
            Assert.Equal(2, returnedProducts.Count);
            Assert.Equal(categoryList[0].Name, returnedProducts.FirstOrDefault()?.Name);
        }

        [Fact]
        public async Task Add_ValidProduct_ReturnsCreated()
        {
            // Arrange
            var expectedCategory = new CategoryDto { Id = 1, Name = "Category_A" };
            _mockCategoryService
                .Setup(service => service.AddAsync(It.IsAny<CategoryDto>()))
                .ReturnsAsync(expectedCategory);

            // Act
            var result = await _categoryController.Add(expectedCategory) as CreatedAtActionResult;

            // Assert
            var category = result?.Value as CategoryDto;
            Assert.NotNull(result);
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(category);
            Assert.Equal(expectedCategory.Id, category.Id);
            Assert.Equal(expectedCategory.Name, category.Name);
            _mockCategoryService.Verify(service => service.AddAsync(It.IsAny<CategoryDto>()), Times.Once);
        }

        [Fact]
        public async Task Update_UpdatedProduct_ReturnsNoContent()
        {
            // Arrange
            var expectedCategory = new CategoryDto { Id = 1, Name = "Category A" };
            _mockCategoryService
                .Setup(service => service.UpdateAsync(It.IsAny<CategoryDto>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _categoryController.Update(1, expectedCategory) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
            _mockCategoryService.Verify(service => service.UpdateAsync(It.IsAny<CategoryDto>()), Times.Once);
        }

        [Fact]
        public async Task Delete_DeletesProduct_ReturnsNoContent()
        {
            // Arrange
            var givenCategoryIdToDelete = 1;
            _mockCategoryService
                .Setup(service => service.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _categoryController.Delete(givenCategoryIdToDelete) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
            _mockCategoryService.Verify(service => service.DeleteAsync(It.Is<int>(x => x == givenCategoryIdToDelete)), Times.Once);
        }
    }
}