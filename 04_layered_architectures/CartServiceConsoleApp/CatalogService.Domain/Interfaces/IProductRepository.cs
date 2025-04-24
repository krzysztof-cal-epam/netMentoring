using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsAsync(int? categoryId, int page, int pageSize);

    }
}
