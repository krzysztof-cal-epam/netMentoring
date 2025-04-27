using CartServiceConsoleApp.DAL.Interfaces;
using CartServiceConsoleApp.Entities;
using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;

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
            var cart = _cartRepository.GetCartById(cartId);

            if (cart == null)
                return null;

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

        public void AddItemToCart(Guid cartId, CartItemDto dto)
        {
            var cart = _cartRepository.GetCartById(cartId) ?? new Cart(cartId);

            var item = cart.Items.FirstOrDefault(i => i.Id == dto.Id);

            if (item != null)
            {
                item.Quantity += dto.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Price = dto.Price,
                    Quantity = dto.Quantity,
                    Image = dto.Image != null
                        ? new ImageItem
                        {
                            ImageUrl = dto.Image.ImageUrl,
                            AltText = dto.Image.AltText
                        }
                        : null
                });
            }

            _cartRepository.SaveCart(cart);
        }

        public void RemoveItemFromCart(Guid cartId, int itemId)
        {
            var cart = _cartRepository.GetCartById(cartId);
            if (cart != null)
            {
                cart.Items.RemoveAll(i => i.Id == itemId);
                _cartRepository.SaveCart(cart);
            }
        }

        public IEnumerable<CartDto> GetAllCarts()
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
    }
}
