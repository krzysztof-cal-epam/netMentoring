using CatalogService.Application.Dto;
using CatalogService.Domain.Interfaces;

namespace CatalogService.Application.Interfaces
{
    public interface IProductService : IRepository<ProductDto>
    {
        Task<IEnumerable<ProductDto>> ListAsync(int? categoryId, int page, int pageSize);
    }
}
