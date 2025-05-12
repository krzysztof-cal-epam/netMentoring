using CartServiceConsoleApp.DAL.Exceptions;
using CartServiceConsoleApp.DAL.Interfaces;
using CartServiceConsoleApp.Entities;
using LiteDB;

namespace CartServiceConsoleApp.DAL.Databases
{
    public class LiteDbCartDatabase : ICartDatabase<Cart>, IDisposable
    {
        private readonly LiteDatabase _db;
        private readonly SemaphoreSlim _semaphore;
        private volatile bool _disposed;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public LiteDbCartDatabase(string connection)
        {
            try
            {
                var connectionString = new ConnectionString(connection) { Connection = ConnectionType.Shared };
                _db = new LiteDatabase(connectionString);
                _semaphore = new SemaphoreSlim(1, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LiteDbCartDatabase] Failed to initialize database: {ex.Message}");
                throw;
            }
        }

        public Cart FindById(Guid id)
        {
            try
            {
                return ExecuteWithLock(() =>
                {
                    CheckDisposed();
                    var collection = _db.GetCollection<Cart>("Carts");
                    return collection.FindById(id);
                });
            }
            catch (LiteException ex)
            {
                Console.WriteLine($"[LiteDbCartDatabase] Error in FindById({id}): {ex.Message}");
                throw new DatabaseReadException($"Failed to find cart with id <{id}>", ex);
            }
        }

        public IEnumerable<Cart> GetAll()
        {
            try
            {
                return ExecuteWithLock(() =>
                {
                    CheckDisposed();
                    var collection = _db.GetCollection<Cart>("Carts");
                    return collection.FindAll();
                });
            }
            catch (LiteException ex)
            {
                Console.WriteLine($"[LiteDbCartDatabase] Error in GetAll: {ex.Message}");
                throw new DatabaseReadException("Failed to get all carts", ex);
            }
        }

        public void Upsert(Cart item)
        {
            try
            {
                ExecuteWithLock(() =>
                {
                    CheckDisposed();
                    var collection = _db.GetCollection<Cart>("Carts");
                    collection.Upsert(item);
                });
            }
            catch (LiteException ex)
            {
                Console.WriteLine($"[LiteDbCartDatabase] Error in Upsert({item.Id}): {ex.Message}");
                throw new DatabaseWriteException($"Failed to upsert cart with id <{item.Id}>", ex);
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                ExecuteWithLock(() =>
                {
                    CheckDisposed();
                    var collection = _db.GetCollection<Cart>("Carts");

                    if (collection.FindById(id) == null)
                    {
                        throw new CartNotFoundException(id);
                    }

                    collection.Delete(id);
                });
            }
            catch (LiteException ex)
            {
                Console.WriteLine($"[LiteDbCartDatabase] Error in Delete({id}): {ex.Message}");
                throw new DatabaseWriteException($"Failed to delete cart with id <{id}>", ex);
            }
        }

        private T ExecuteWithLock<T>(Func<T> action)
        {
            CheckDisposed();

            try
            {
                _cts.Token.ThrowIfCancellationRequested();
                if (!_semaphore.Wait(Timeout.Infinite, _cts.Token))
                {
                    throw new OperationCanceledException("Semaphore wait was canceled.");
                }
                try
                {
                    return action();
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"[LiteDbCartDatabase] Semaphore disposed: {ex.Message}");
                throw;
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"[LiteDbCartDatabase] Operation canceled: {ex.Message}");
                throw;
            }
        }

        private void ExecuteWithLock(Action action)
        {
            CheckDisposed();
            try
            {
                _cts.Token.ThrowIfCancellationRequested();
                if (!_semaphore.Wait(Timeout.Infinite, _cts.Token))
                {
                    throw new OperationCanceledException("Semaphore wait was canceled.");
                }
                try
                {
                    action();
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"[LiteDbCartDatabase] Semaphore disposed: {ex.Message}");
                throw;
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"[LiteDbCartDatabase] Operation canceled: {ex.Message}");
                throw;
            }
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(LiteDbCartDatabase), "Database engine instance has been disposed.");
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                try
                {
                    _cts.Cancel();
                    _db?.Dispose();
                    Console.WriteLine("[LiteDbCartDatabase] Database disposed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LiteDbCartDatabase] Error during disposal: {ex.Message}");
                }
                try
                {
                    _semaphore?.Dispose();
                    Console.WriteLine("[LiteDbCartDatabase] Semaphore disposed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LiteDbCartDatabase] Error disposing semaphore: {ex.Message}");
                }
                try
                {
                    _cts?.Dispose();
                    Console.WriteLine("[LiteDbCartDatabase] CancellationTokenSource disposed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LiteDbCartDatabase] Error disposing CancellationTokenSource: {ex.Message}");
                }
                _disposed = true;
            }
        }
    }
}
