
namespace CatalogService.Application.Dto
{
    public class CartDto
    {
        public Guid CartId { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
