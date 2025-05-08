using System.ComponentModel.DataAnnotations;

namespace CatalogService.Application.Exceptions
{
    public class CartValidationException : ValidationException
    {
        public CartValidationException(string message) : base(message) { }
    }
}
