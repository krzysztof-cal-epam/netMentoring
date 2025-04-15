using CartServiceConsoleApp.DAL.Interfaces;
using CartServiceConsoleApp.Entities;

namespace CartServiceConsoleApp.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ICartDatabase<Cart> _database;

        public CartRepository(ICartDatabase<Cart> database)
        {
            _database = database;
        }

        public Cart GetCartById(Guid cartId)
        {
            return _database.FindById(cartId);
        }

        public void SaveCart(Cart cart)
        {
            _database.Upsert(cart);
        }

        public void DeleteCart(Guid cartId)
        {
            _database.Delete(cartId);
        }

        public IEnumerable<Cart> GetAllCarts()
        {
            return _database.GetAll();
        }

        public void Dispose()
        {
            if (_database is IDisposable disposableDb)
            {
                disposableDb.Dispose();
            }
        }
    }
}
