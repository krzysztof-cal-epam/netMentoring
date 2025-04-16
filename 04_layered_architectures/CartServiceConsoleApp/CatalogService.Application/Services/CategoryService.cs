using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;

namespace CatalogService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Image = categoryDto.Image,
                ParentCategoryId = categoryDto.ParentCategoryId
            };

            await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new Exception($"Category with ID {id} not found.");

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
                ParentCategoryId = category.ParentCategoryId
            };
        }

        public async Task<IEnumerable<CategoryDto>> ListAsync()
        {
            var categories = await _categoryRepository.ListAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Image = c.Image,
                ParentCategoryId = c.ParentCategoryId
            });
        }

        public async Task UpdateAsync(CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);
            if (category == null)
                throw new Exception($"Category with ID {categoryDto.Id} not found.");

            category.Name = categoryDto.Name;
            category.Image = categoryDto.Image;
            category.ParentCategoryId = categoryDto.ParentCategoryId;

            await _categoryRepository.UpdateAsync(category);
        }
    }
}
