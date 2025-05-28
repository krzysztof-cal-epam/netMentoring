namespace CatalogService.Domain.Exceptions
{
    public class DatabaseReadException : Exception
    {
        public DatabaseReadException(string message, Exception innerException = null) : base(message, innerException) { }
    }

    public class DatabaseWriteException : Exception
    {
        public DatabaseWriteException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}
