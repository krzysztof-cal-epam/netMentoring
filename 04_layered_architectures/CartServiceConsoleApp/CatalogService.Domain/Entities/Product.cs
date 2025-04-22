using System.ComponentModel.DataAnnotations;

namespace CatalogService.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Uri? Image { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public uint Amount { get; set; }

        [Required]
        public int CategoryId { get; set; }
        
        [Required]
        public Category Category { get; set; }
    }
}
