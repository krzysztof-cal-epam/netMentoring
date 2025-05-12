using CatalogService.Application.Dto;

namespace CatalogService.Application.Interfaces
{
    public interface ICartService
    {
        CartDto GetCartInfo(Guid cartId);
        void AddItemToCart(Guid cartId, CartItemDto item);
        void RemoveItemFromCart(Guid cartId, int itemId);
        IEnumerable<CartDto> GetAllCarts();
        void UpdateCartItems(int productId, string? updatedName, decimal? updatedPrice);
    }
}
