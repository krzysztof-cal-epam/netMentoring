using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces;

namespace CatalogService.Domain.Repositories
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
            try
            {

                var cart = _database.FindById(cartId);
                if (cart == null)
                {
                    throw new CartNotFoundException(cartId);
                }

                return cart;
            }
            catch (DatabaseReadException ex)
            {
                throw new RepositoryException("Repository failed to get the cart.", ex);
            }
        }

        public void SaveCart(Cart cart)
        {
            try
            {
                _database.Upsert(cart);
            }
            catch (DatabaseWriteException ex)
            {
                throw new RepositoryException("Repository failed to save cart", ex);
            }
        }

        public void DeleteCart(Guid cartId)
        {
            try
            {
                _database.Delete(cartId);
            }
            catch (CartNotFoundException ex)
            {
                throw;
            }
            catch (DatabaseWriteException ex)
            {
                throw new RepositoryException("Repository failed to delete cart", ex);
            }
        }

        public IEnumerable<Cart> GetAllCarts()
        {
            try
            {
                return _database.GetAll();
            }
            catch (DatabaseReadException ex)
            {
                throw new RepositoryException("Repository failed to get all carts", ex);
            }
        }
    }
}
