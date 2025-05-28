using CatalogService.Application.Dto;
using CatalogService.Domain.Interfaces;

namespace CatalogService.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> ListAsync();
        Task<ProductDto> AddAsync(ProductDto entity);
        Task UpdateAsync(ProductDto entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<ProductDto>> ListAsync(int? categoryId, int page, int pageSize);
    }
}
