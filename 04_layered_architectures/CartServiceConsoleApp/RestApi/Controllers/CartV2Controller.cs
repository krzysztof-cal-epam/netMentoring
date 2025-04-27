using CartServiceConsoleApp.Entities;
using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/v2/cart")]
    public class CartV2Controller : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartV2Controller(ICartService cartService) 
        {
            _cartService = cartService;

        }

        [HttpGet("{cartId}")]
        public new IActionResult GetCartInfo(Guid cartId)
        {
            var cart = _cartService.GetCartInfo(cartId);
            if (cart == null) return NotFound();
            return Ok(cart.Items);
        }

        [HttpPost("{cartId}/items")]
        public IActionResult AddToCart(Guid cartId, [FromBody] CartItemDto item)
        {
            _cartService.AddItemToCart(cartId, item);
            return Ok();
        }

        [HttpDelete("{cartId}/items/{itemId}")]
        public IActionResult RemoveItem(Guid cartId, int itemId)
        {
            _cartService.RemoveItemFromCart(cartId, itemId);
            return Ok();
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Cart>> GetAllCarts()
        {
            var carts = _cartService.GetAllCarts();
            return Ok(carts);
        }
    }
}
