using CartServiceConsoleApp.Entities;

namespace CartServiceConsoleApp.DAL.Interfaces
{
    public interface ICartRepository : IDisposable
    {
        Cart GetCartById(string cartId);
        void SaveCart(Cart cart);
        void DeleteCart(string cartId);
    }
}
