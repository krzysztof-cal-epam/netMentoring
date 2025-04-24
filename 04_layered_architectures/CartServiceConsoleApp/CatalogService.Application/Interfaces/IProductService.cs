using CatalogService.Application.Dto;
using CatalogService.Domain.Interfaces;

namespace CatalogService.Application.Interfaces
{
    public interface IProductService : IRepository<ProductDto>
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(int? categoryId, int page, int pageSize);
    }
}
