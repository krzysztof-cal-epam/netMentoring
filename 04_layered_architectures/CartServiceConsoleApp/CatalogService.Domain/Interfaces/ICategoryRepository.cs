using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task DeleteWithProductsAsync(int parentId);
    }
}
