using CatalogService.Domain.Entities;

namespace CatalogService.DataAccess.Interfaces
{
    public interface ICartService
    {
        List<CartItem> GetItems(Guid cartId);
        void AddItem(Guid cartId, CartItem item);
        void RemoveItem(Guid cartId, int itemId);
        IEnumerable<Cart> GetAllCarts();
    }
}
