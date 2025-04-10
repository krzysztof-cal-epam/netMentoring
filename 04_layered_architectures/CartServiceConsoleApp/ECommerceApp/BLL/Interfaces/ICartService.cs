using CartServiceConsoleApp.Entities;

namespace CartServiceConsoleApp.BLL.Interfaces
{
    public interface ICartService
    {
        List<CartItem> GetItems(string cartId);
        void AddItem(string cartId, CartItem item);
        void RemoveItem(string cartId, string itemId);
    }
}
