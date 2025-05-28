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

        public async Task DeleteWithProductsAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    throw new KeyNotFoundException($"Category with ID {id} does not exist.");
                }

                var productsToDelete = await _context.Products.Where(p => p.CategoryId == id).ToListAsync();

                //todo add delition of child categories

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
            }
        }
    }
}
