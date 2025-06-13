using CatalogService.DataAccess.Data;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly CatalogDbContext _context;

        public CategoryRepository(CatalogDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteWithProductsAsync(int parentId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == parentId);
                if (category == null)
                {
                    throw new KeyNotFoundException($"Category with ID {parentId} does not exist.");
                }

                var childCategories = new List<Category>();
                await GetChildCategoriesRecursiveAsync(parentId, childCategories);

                var childCategoryIds = childCategories.Select(c => c.Id).ToList();
                var productsInChildCategories = await _context.Products
                    .Where(p => childCategoryIds.Contains(p.CategoryId))
                    .ToListAsync();

                if (productsInChildCategories.Any())
                {
                    _context.Products.RemoveRange(productsInChildCategories);
                }

                if (childCategories.Any())
                {
                    _context.Categories.RemoveRange(childCategories);
                }

                var productsToDelete = await _context.Products.Where(p => p.CategoryId == parentId).ToListAsync();

                if (productsToDelete.Any())
                {
                    _context.Products.RemoveRange(productsToDelete);
                }

                _context.Categories.Remove(category);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task GetChildCategoriesRecursiveAsync(int parentCategoryId, List<Category> childCategories)
        {
            var directChildCategories = await _context.Categories
                .Where(c => c.ParentCategoryId == parentCategoryId)
                .ToListAsync();

            foreach (var child in directChildCategories)
            {
                childCategories.Add(child);

                await GetChildCategoriesRecursiveAsync(child.Id, childCategories);
            }
        }
    }
}
