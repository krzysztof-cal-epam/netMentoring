using CatalogService.DataAccess.Data;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataAccess.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly CatalogDbContext _context;

        public ProductRepository(CatalogDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> ListAsync(int? categoryId, int page, int pageSize)
        {
            var query = _context.Set<Product>().AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task UpdateProductWithOutboxAsync(Product product, string eventType, object payload)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Set<Product>().Update(product);

                await AddOutboxEventAsync(eventType, payload);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
