using CartServiceConsoleApp.Entities;
using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    /// <summary>
    /// Cart Api V1
    /// </summary>
    [ApiController]
    [Route("api/v1/cart")]
    public class CartV1Controller : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartV1Controller(ICartService cartService)
        {
            _cartService = cartService;
        }
        /// <summary>
        /// Gets full CartInfo
        /// </summary>
        /// <param name="cartId">Unique Guid of a cart</param>
        /// <returns>Status 200 ok</returns>
        [HttpGet("{cartId}")]
        public IActionResult GetCartInfo(Guid cartId)
        {
            var cart = _cartService.GetCartInfo(cartId);
            return Ok(cart);
        }

        /// <summary>
        /// Adds an item to the cart
        /// </summary>
        /// <param name="cartId">Unique Guid of a cart</param>
        /// <param name="item">Item do be added</param>
        /// <returns>Status 200 ok</returns>
        [HttpPost("{cartId}/items")]
        public IActionResult AddToCart(Guid cartId, [FromBody] CartItemDto item)
        {
            _cartService.AddItemToCart(cartId, item);
            return Ok();
        }

        /// <summary>
        /// Removes item from a given cart 
        /// </summary>
        /// <param name="cartId">Unique Guid of a cart</param>
        /// <param name="itemId">Id of an item to be removed</param>
        /// <returns>Status 200 ok</returns>
        [HttpDelete("{cartId}/items/{itemId}")]
        public IActionResult RemoveItem(Guid cartId, int itemId)
        {
            _cartService.RemoveItemFromCart(cartId, itemId);
            return Ok();
        }

        /// <summary>
        /// Returns all carts
        /// </summary>
        /// <returns>Status 200 ok</returns>
        [HttpGet("all")]
        public ActionResult<IEnumerable<Cart>> GetAllCarts()
        {
            var carts = _cartService.GetAllCarts();
            return Ok(carts);
        }
    }
}
