using CartServiceConsoleApp.DAL.Interfaces;
using CartServiceConsoleApp.Entities;
using LiteDB;

namespace CartServiceConsoleApp.DAL.Repositories
{
    //todo extract some abstraction and make reposiorty testable with any db
    //todo consider config file to make connection secure (not hardcoded)
    public class CartRepository : ICartRepository
    {
        private readonly LiteDatabase _db;

        public CartRepository(string connection = "ECommerceDb.db")
        {
            var connectionString = new ConnectionString(connection) { Connection = ConnectionType.Shared };
            _db = new LiteDatabase(connectionString);
        }

        public void DeleteCart(string cartId)
        {
            var collection = _db.GetCollection<Cart>("Carts");
            collection.Delete(cartId);
        }

        public Cart GetCartById(string cartId)
        {
            var collection = _db.GetCollection<Cart>("Carts");
            return collection.FindById(cartId);
        }

        public void SaveCart(Cart cart)
        {
            var collection = _db.GetCollection<Cart>("Carts");
            collection.Upsert(cart);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
