using CatalogService.DataAccess.Data;
using CatalogService.DataAccess.Repositories;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CatalogService.Tests.Repositories
{
    public class CategoryRepositoryTests
    {
        [Fact]
        public async Task DeleteWithProductsAsync_DeletesCategoryAndAssociatedProducts()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Test Category" };
            var products = new List<Product>
            {
                new Product { Id = 101, Name = "Product 1", CategoryId = categoryId },
                new Product { Id = 102, Name = "Product 2", CategoryId = categoryId }
            };

            var options = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase(databaseName: "CatalogDbWithSeedData")
                .ConfigureWarnings(warn => warn.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using var context = new CatalogDbContext(options);

            await context.Categories.AddAsync(category);
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();

            var categoryRepository = new CategoryRepository(context);

            // Act
            await categoryRepository.DeleteWithProductsAsync(categoryId);

            // Assert
            Assert.Null(await context.Categories.FindAsync(categoryId));
            Assert.Empty(await context.Products.Where(p => p.CategoryId == categoryId).ToListAsync());
        }
    }
}
