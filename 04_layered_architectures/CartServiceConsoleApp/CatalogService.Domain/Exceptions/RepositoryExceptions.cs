namespace CatalogService.Domain.Exceptions
{
    public class CartNotFoundException : Exception
    {
        public CartNotFoundException(Guid cartId) : base($"Cart with the id <{cartId}> was not found") { }
    }

    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(int itemId, Guid cartId) : base($"Item with the id <{itemId}> was not found in cart with id <{cartId}>") { }

    }

    public class RepositoryException : Exception
    {
        public RepositoryException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}
