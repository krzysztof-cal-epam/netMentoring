using CatalogService.DataAccess.Interfaces;
using CatalogService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CartServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/cart/{cartId}
        [HttpGet("{cartId}")]
        public ActionResult<List<CartItem>> GetCartItems(Guid cartId)
        {
            var cartItems = _cartService.GetItems(cartId);
            if (cartItems == null || cartItems.Count == 0)
            {
                return NotFound($"Cart with ID {cartId} not found or is empty.");
            }

            return Ok(cartItems);
        }

        // POST: api/cart/{cartId}/items
        [HttpPost("{cartId}/items")]
        public ActionResult AddItemToCart(Guid cartId, [FromBody] CartItem cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest("CartItem cannot be null.");
            }

            _cartService.AddItem(cartId, cartItem);
            return Ok($"Item added to cart {cartId}.");
        }

        // DELETE: api/cart/{cartId}/items/{itemId}
        [HttpDelete("{cartId}/items/{itemId}")]
        public ActionResult RemoveItemFromCart(Guid cartId, int itemId)
        {
            var cart = _cartService.GetItems(cartId);
            if (cart == null || cart.Count == 0)
            {
                return NotFound($"Cart with ID {cartId} not found or is empty.");
            }

            _cartService.RemoveItem(cartId, itemId);
            return Ok($"Item {itemId} removed from cart {cartId}.");
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Cart>> GetAllCarts()
        {
            var carts = _cartService.GetAllCarts();
            return Ok(carts);
        }
    }
}
