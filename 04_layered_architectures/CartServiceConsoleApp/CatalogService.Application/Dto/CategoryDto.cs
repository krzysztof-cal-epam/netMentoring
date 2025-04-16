using System.ComponentModel.DataAnnotations;

namespace CatalogService.Application.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public Uri? ImageUrl { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
