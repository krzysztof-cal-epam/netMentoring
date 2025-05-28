using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Interfaces
{
    public interface ICartRepository
    {
        Cart GetCartById(Guid cartId);
        void SaveCart(Cart cart);
        void DeleteCart(Guid cartId);
        IEnumerable<Cart> GetAllCarts();
    }
}
