using CatalogService.Application.Dto;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces;

namespace CatalogService.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public CartDto GetCartInfo(Guid cartId)
        {
            if (cartId == Guid.Empty)
            {
                throw new CartValidationException("Invalid cart ID: must not be empty.");
            }

            try
            {
                var cart = _cartRepository.GetCartById(cartId);
                return new CartDto
                {
                    CartId = cart.Id,
                    Items = cart.Items.Select(item => new CartItemDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Image = item.Image != null
                            ? new ImageItemDto
                            {
                                ImageUrl = item.Image.ImageUrl,
                                AltText = item.Image.AltText,
                            }
                            : null,
                    }).ToList(),
                };
            }
            catch (CartNotFoundException ex)
            {
                throw;
            }
            catch (RepositoryException ex)
            {
                throw new ApplicationException("An unexpected error occurred in the application layer GetCartInfo", ex);
            }
        }

        public void AddItemToCart(Guid cartId, CartItemDto item)
        {
            if (cartId == Guid.Empty)
            {
                throw new CartValidationException("CartId should not be empty");
            }

            if (item == null || string.IsNullOrEmpty(item.Name))
            {
                throw new CartValidationException("Item must have a name");
            }

            try
            {
                Cart? cart = null;
                try
                {
                    cart = _cartRepository.GetCartById(cartId);
                }
                catch (CartNotFoundException ex)
                {
                    cart = new Cart(cartId);
                }

                var firstItem = cart.Items.FirstOrDefault(i => i.Id == item.Id);

                if (firstItem != null)
                {
                    firstItem.Quantity += item.Quantity;
                }
                else
                {
                    cart.Items.Add(new CartItem
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Image = item.Image != null
                            ? new ImageItem
                            {
                                ImageUrl = item.Image.ImageUrl,
                                AltText = item.Image.AltText,
                            }
                            : null,
                    });
                }

                _cartRepository.SaveCart(cart);
            }
            catch (RepositoryException ex)
            {
                throw new ApplicationException("An unexpected error occurred in the application layer AddItemToCart", ex);
            }
        }

        public void RemoveItemFromCart(Guid cartId, int itemId)
        {
            if (cartId == Guid.Empty)
            {
                throw new CartValidationException("CartId should not be empty");
            }

            if (itemId <= 0)
            {
                throw new CartValidationException("Item id must be greater than zero.");
            }

            try
            {
                var cart = _cartRepository.GetCartById(cartId);
                if (cart != null)
                {
                    if (!cart.Items.Any(i => i.Id == itemId))
                    {
                        throw new ItemNotFoundException(itemId, cartId);
                    }
                    cart.Items.RemoveAll(i => i.Id == itemId);
                    _cartRepository.SaveCart(cart);
                }
            }
            catch (CartNotFoundException ex)
            {
                throw;
            }
            catch (RepositoryException ex)
            {
                throw new ApplicationException("An unexpected error occurred in the application layer RemoveItemFromCart", ex);
            }
        }

        public IEnumerable<CartDto> GetAllCarts()
        {
            try
            {
                var allCarts = _cartRepository.GetAllCarts();

                return allCarts.Select(cart => new CartDto
                {
                    CartId = cart.Id,
                    Items = cart.Items.Select(item => new CartItemDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity
                    }).ToList()
                });
            }
            catch (RepositoryException ex)
            {
                throw new ApplicationException("An unexpected error occurred in the application layer GetAllCarts", ex);
            }
        }

        public void UpdateCartItems(int productId, string? updatedName, decimal? updatedPrice)
        {
            ValidateProductId(productId);

            try
            {
                var cartsToUpdate = FindCartsToUpdate(productId, updatedName, updatedPrice);
                SaveUpdatedCarts(cartsToUpdate);
            }
            catch (ObjectDisposedException ex)
            {
                LogAndThrowDatabaseError(ex, "Database disposed during UpdateCartItems");
            }
            catch (OperationCanceledException ex)
            {
                LogAndThrowOperationCanceled(ex, "Operation canceled in UpdateCartItems");
            }
            catch (RepositoryException ex)
            {
                throw new ApplicationException("An unexpected error occurred while updating cart items.", ex);
            }
        }

        private void ValidateProductId(int productId)
        {
            if (productId <= 0)
            {
                throw new CartValidationException("ProductId must be greater than zero.");
            }
        }

        private List<Cart> FindCartsToUpdate(int productId, string? updatedName, decimal? updatedPrice)
        {
            var cartsToUpdate = new List<Cart>();
            var allCarts = _cartRepository.GetAllCarts().ToList();

            foreach (var cart in allCarts)
            {
                if (UpdateCartItems(cart, productId, updatedName, updatedPrice))
                {
                    cartsToUpdate.Add(cart);
                }
            }

            return cartsToUpdate;
        }

        private bool UpdateCartItems(Cart cart, int productId, string? updatedName, decimal? updatedPrice)
        {
            bool isUpdated = false;

            foreach (var item in cart.Items)
            {
                if (item.Id == productId)
                {
                    UpdateItem(item, updatedName, updatedPrice);
                    isUpdated = true;
                }
            }

            return isUpdated;
        }

        private void UpdateItem(CartItem item, string? updatedName, decimal? updatedPrice)
        {
            if (!string.IsNullOrEmpty(updatedName))
            {
                item.Name = updatedName;
            }
            if (updatedPrice.HasValue)
            {
                item.Price = updatedPrice.Value;
            }
        }

        private void SaveUpdatedCarts(List<Cart> carts)
        {
            foreach (var cart in carts)
            {
                _cartRepository.SaveCart(cart);
            }
        }

        private void LogAndThrowDatabaseError(ObjectDisposedException ex, string message)
        {
            Console.WriteLine($"[CartService] {message}: {ex.Message}");
            throw new ApplicationException("Database is no longer available.", ex);
        }

        private void LogAndThrowOperationCanceled(OperationCanceledException ex, string message)
        {
            Console.WriteLine($"[CartService] {message}: {ex.Message}");
            throw new ApplicationException("Operation was canceled.", ex);
        }
    }
}