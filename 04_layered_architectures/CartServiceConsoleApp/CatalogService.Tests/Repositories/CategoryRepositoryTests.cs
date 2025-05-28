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

        [Fact]
        public async Task DeleteWithProductsAsync_DeletesParentCategory_ChildCategories_AndAllAssociatedProducts()
        {
            // Arrange
            var parentCategoryId = 1;
            var childCategoryId1 = 2;
            var childCategoryId2 = 3;

            var parentCategory = new Category { Id = parentCategoryId, Name = "Parent Category" };
            var childCategory1 = new Category
            { Id = childCategoryId1, Name = "Child Category 1", ParentCategoryId = parentCategoryId };
            var childCategory2 = new Category
            { Id = childCategoryId2, Name = "Child Category 2", ParentCategoryId = parentCategoryId };

            var parentProducts = new List<Product>
            {
                new Product { Id = 101, Name = "Parent Product 1", CategoryId = parentCategoryId },
                new Product { Id = 102, Name = "Parent Product 2", CategoryId = parentCategoryId }
            };
            var childCategory1Products = new List<Product>
            {
                new Product { Id = 201, Name = "Child1 Product 1", CategoryId = childCategoryId1 },
                new Product { Id = 202, Name = "Child1 Product 2", CategoryId = childCategoryId1 }
            };
            var childCategory2Products = new List<Product>
            {
                new Product { Id = 301, Name = "Child2 Product 1", CategoryId = childCategoryId2 }
            };

            var options = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase(databaseName: "CatalogDbWithChildCategories")
                .ConfigureWarnings(warn => warn.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using var context = new CatalogDbContext(options);

            await context.Categories.AddAsync(parentCategory);
            await context.Categories.AddRangeAsync(childCategory1, childCategory2);
            await context.Products.AddRangeAsync(parentProducts);
            await context.Products.AddRangeAsync(childCategory1Products);
            await context.Products.AddRangeAsync(childCategory2Products);
            await context.SaveChangesAsync();

            var categoryRepository = new CategoryRepository(context);

            // Act
            await categoryRepository.DeleteWithProductsAsync(parentCategoryId);

            // Assert
            Assert.Null(await context.Categories.FindAsync(parentCategoryId));
            Assert.Null(await context.Categories.FindAsync(childCategoryId1));
            Assert.Null(await context.Categories.FindAsync(childCategoryId2));

            Assert.Empty(await context.Products.Where(p => p.CategoryId == parentCategoryId).ToListAsync());
            
            Assert.Empty(await context.Products.Where(p => p.CategoryId == childCategoryId1).ToListAsync());
            Assert.Empty(await context.Products.Where(p => p.CategoryId == childCategoryId2).ToListAsync());
        }
    }
}
