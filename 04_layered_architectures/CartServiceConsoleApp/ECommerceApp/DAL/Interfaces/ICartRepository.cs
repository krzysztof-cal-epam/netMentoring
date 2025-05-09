﻿using CartServiceConsoleApp.Entities;

namespace CartServiceConsoleApp.DAL.Interfaces
{
    public interface ICartRepository : IDisposable
    {
        Cart GetCartById(Guid cartId);
        void SaveCart(Cart cart);
        void DeleteCart(Guid cartId);
        IEnumerable<Cart> GetAllCarts();
    }
}
