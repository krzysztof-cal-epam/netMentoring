using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;

namespace CatalogService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        public ProductService(IProductRepository productRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductDto> AddAsync(ProductDto entity)
        {
            var category = await _categoryRepository.GetByIdAsync(entity.CategoryId);
            if (category == null)
                throw new Exception($"Category with ID {entity.CategoryId} does not exist.");

            var product = new Product
            {
                Name = entity.Name,
                Description = entity.Description,
                Image = entity.Image,
                Price = entity.Price,
                Amount = entity.Amount,
                CategoryId = entity.CategoryId
            };

            var res = await _productRepository.AddAsync(product);

            if (res == null)
            {
                throw new Exception("Failed to save product");
            }

            return new ProductDto
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                Price = res.Price,
                Amount = res.Amount,
                CategoryId = res.CategoryId
            };
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception($"Product with ID {id} not found.");

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Amount = product.Amount,
                CategoryId = product.CategoryId
            };
        }

        public async Task<IEnumerable<ProductDto>> ListAsync(int? categoryId, int page, int pageSize)
        {
            var products = await _productRepository.ListAsync(categoryId, page, pageSize);

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Amount = p.Amount,
                CategoryId = p.CategoryId,
            }).ToList();
        }

        public async Task<IEnumerable<ProductDto>> ListAsync()
        {
            var products = await _productRepository.ListAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Image = p.Image,
                Price = p.Price,
                Amount = p.Amount,
                CategoryId = p.CategoryId
            });
        }

        public async Task UpdateAsync(ProductDto entity)
        {
            var product = await _productRepository.GetByIdAsync(entity.Id);
            if (product == null)
                throw new Exception($"Product with ID {entity.Id} not found.");

            var category = await _categoryRepository.GetByIdAsync(entity.CategoryId);
            if (category == null)
                throw new Exception($"Category with ID {entity.CategoryId} does not exist.");

            product.Name = entity.Name;
            product.Description = entity.Description;
            product.Image = entity.Image;
            product.Price = entity.Price;
            product.Amount = entity.Amount;
            product.CategoryId = entity.CategoryId;

            var eventPayload = new
            {
                ItemId = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            await _productRepository.UpdateProductWithOutboxAsync(product, "ProductUpdated", eventPayload);
        }
    }
}
