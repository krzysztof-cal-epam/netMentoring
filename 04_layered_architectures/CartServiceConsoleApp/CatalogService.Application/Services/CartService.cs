﻿using CatalogService.Application.Dto;
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
                                AltText = item.Image.AltText
                            }
                            : null
                    }).ToList()
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

        public void AddItemToCart(Guid cartId, CartItemDto cartItemDto)
        {
            if (cartId == Guid.Empty)
            {
                throw new CartValidationException("CartId should not be empty");
            }

            if (cartItemDto == null || string.IsNullOrEmpty(cartItemDto.Name))
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
                catch(CartNotFoundException ex)
                {
                    cart = new Cart(cartId);
                }

                var item = cart.Items.FirstOrDefault(i => i.Id == cartItemDto.Id);

                if (item != null)
                {
                    item.Quantity += cartItemDto.Quantity;
                }
                else
                {
                    cart.Items.Add(new CartItem
                    {
                        Id = cartItemDto.Id,
                        Name = cartItemDto.Name,
                        Price = cartItemDto.Price,
                        Quantity = cartItemDto.Quantity,
                        Image = cartItemDto.Image != null
                            ? new ImageItem
                            {
                                ImageUrl = cartItemDto.Image.ImageUrl,
                                AltText = cartItemDto.Image.AltText
                            }
                            : null
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
            if (productId <= 0)
            {
                throw new CartValidationException("ProductId must be greater than zero.");
            }

            try
            {
                var allCarts = _cartRepository.GetAllCarts().ToList();
                var cartsToUpdate = new List<Cart>();

                foreach (var cart in allCarts)
                {
                    bool isUpdated = false;
                    foreach (var item in cart.Items)
                    {
                        if (item.Id == productId)
                        {
                            if (!string.IsNullOrEmpty(updatedName))
                            {
                                item.Name = updatedName;
                            }
                            if (updatedPrice.HasValue)
                            {
                                item.Price = updatedPrice.Value;
                            }
                            isUpdated = true;
                        }
                    }
                    if (isUpdated)
                    {
                        cartsToUpdate.Add(cart);
                    }
                }

                foreach (var cart in cartsToUpdate)
                {
                    _cartRepository.SaveCart(cart);
                }
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"[CartService] Database disposed during UpdateCartItems: {ex.Message}");
                throw new ApplicationException("Database is no longer available.", ex);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"[CartService] Operation canceled in UpdateCartItems: {ex.Message}");
                throw new ApplicationException("Operation was canceled.", ex);
            }
            catch (RepositoryException ex)
            {
                throw new ApplicationException("An unexpected error occurred while updating cart items.", ex);
            }
        }
    }
}
