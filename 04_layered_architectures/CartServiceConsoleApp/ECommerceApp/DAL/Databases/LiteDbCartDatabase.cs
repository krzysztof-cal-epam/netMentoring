using CartServiceConsoleApp.DAL.Exceptions;
using CartServiceConsoleApp.DAL.Interfaces;
using CartServiceConsoleApp.Entities;
using LiteDB;

namespace CartServiceConsoleApp.DAL.Databases
{
    public class LiteDbCartDatabase : ICartDatabase<Cart>, IDisposable
    {
        private readonly LiteDatabase _db;

        public LiteDbCartDatabase(string connection)
        {
            var connectionString = new ConnectionString(connection) { Connection = ConnectionType.Shared };
            _db = new LiteDatabase(connectionString);
        }

        public Cart FindById(Guid id)
        {
            try
            {
                var collection = _db.GetCollection<Cart>("Carts");
                return collection.FindById(id);
            }
            catch (LiteException ex)
            {
                throw new DatabaseReadException($"Failed to find cart with id <{id}>", ex);
            }
        }

        public IEnumerable<Cart> GetAll()
        {
            try
            {
                var collection = _db.GetCollection<Cart>("Carts");
                return collection.FindAll();
            }
            catch (LiteException ex)
            {
                throw new DatabaseReadException("Failed to get all carts", ex);
            }
        }

        public void Upsert(Cart item)
        {
            try
            {
                var collection = _db.GetCollection<Cart>("Carts");
                collection.Upsert(item);
            }
            catch (LiteException ex)
            {
                throw new DatabaseWriteException($"Failed to upsert cart with id <{item.Id}>", ex);
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                var collection = _db.GetCollection<Cart>("Carts");

                if (collection.FindById(id) == null)
                {
                    throw new CartNotFoundException(id);
                }

                collection.Delete(id);
            }
            catch (Exception ex)
            {
                throw new DatabaseWriteException($"Failed to delete cart with id <{id}>", ex);
            }
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
