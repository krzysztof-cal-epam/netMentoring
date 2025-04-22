using System.ComponentModel.DataAnnotations;

namespace CatalogService.Application.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Uri? Image { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, uint.MaxValue, ErrorMessage = "Amount must be at least 1.")]
        public uint Amount { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
