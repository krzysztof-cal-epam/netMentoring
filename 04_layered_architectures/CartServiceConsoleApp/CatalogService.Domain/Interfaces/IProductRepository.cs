using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> ListAsync(int? categoryId, int page, int pageSize);

    }
}
