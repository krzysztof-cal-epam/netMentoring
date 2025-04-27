
namespace CatalogService.Application.Dto
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ImageItemDto? Image { get; set; } 
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
    }
}
