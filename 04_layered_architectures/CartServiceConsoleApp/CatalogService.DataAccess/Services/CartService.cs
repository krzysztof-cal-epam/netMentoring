﻿using CatalogService.DataAccess.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces;

namespace CatalogService.DataAccess.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddItem(Guid cartId, CartItem cartItem)
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

            var existingCartItem = cart.Items.FirstOrDefault(i => i.Id == cartItem.Id);

            if (existingCartItem == null)
            {
                cart.Items.Add(cartItem);
            }
            else
            {
                existingCartItem.Quantity += cartItem.Quantity;
            }

            _cartRepository.SaveCart(cart);
        }

        public IEnumerable<Cart> GetAllCarts()
        {
            return _cartRepository.GetAllCarts();
        }

        public List<CartItem> GetItems(Guid cartId)
        {
            var cart = _cartRepository.GetCartById(cartId);
            return cart?.Items ?? new List<CartItem>();
        }

        public void RemoveItem(Guid cartId, int itemId)
        {
            var cart = _cartRepository.GetCartById(cartId);
            if (cart?.Items != null)
            {
                cart.Items.RemoveAll(i => i.Id == itemId);
                _cartRepository.SaveCart(cart);
            }
        }
    }
}
