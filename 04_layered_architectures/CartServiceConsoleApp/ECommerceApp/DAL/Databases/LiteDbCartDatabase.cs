using CartServiceConsoleApp.DAL.Interfaces;
using CartServiceConsoleApp.Entities;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace CartServiceConsoleApp.DAL.Databases
{
    public class LiteDbCartDatabase : ICartDatabase<Cart>, IDisposable
    {
        private readonly LiteDatabase _db;

        public LiteDbCartDatabase(IConfiguration configuration)
        {
            var connection = configuration["LiteDb:DatabasePath"];
            var connectionString = new ConnectionString(connection) { Connection = ConnectionType.Shared };
            _db = new LiteDatabase(connectionString);
        }

        public Cart FindById(Guid id)
        {
            var collection = _db.GetCollection<Cart>("Carts");
            return collection.FindById(id);
        }

        public IEnumerable<Cart> GetAll()
        {
            var collection = _db.GetCollection<Cart>("Carts");
            return collection.FindAll();
        }

        public void Upsert(Cart item)
        {
            var collection = _db.GetCollection<Cart>("Carts");
            collection.Upsert(item);
        }

        public void Delete(Guid id)
        {
            var collection = _db.GetCollection<Cart>("Carts");
            collection.Delete(id);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
