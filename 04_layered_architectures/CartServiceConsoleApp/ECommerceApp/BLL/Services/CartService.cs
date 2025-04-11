using CartServiceConsoleApp.BLL.Interfaces;
using CartServiceConsoleApp.DAL.Interfaces;
using CartServiceConsoleApp.Entities;

namespace CartServiceConsoleApp.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddItem(string cartId, CartItem cartItem)
        {
            var cart = _cartRepository.GetCartById(cartId) ?? new Cart(cartId);

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

        public List<CartItem> GetItems(string cartId)
        {
            var cart = _cartRepository.GetCartById(cartId);
            return cart?.Items ?? new List<CartItem>();
        }

        public void RemoveItem(string cartId, string itemId)
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
