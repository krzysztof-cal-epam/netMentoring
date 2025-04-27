using CartServiceConsoleApp.BLL.Interfaces;
using CartServiceConsoleApp.BLL.Services;
using CartServiceConsoleApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/v1/cart")]
    public class CartV1Controller : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartV1Controller(ICartService cartService)
        {
            _cartService = cartService;
        }

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
    }
}
