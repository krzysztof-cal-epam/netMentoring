using CartServiceConsoleApp.DAL.Exceptions;
using CartServiceConsoleApp.Entities;
using CatalogService.Application.Dto;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
            try
            {
                var cart = _cartService.GetCartInfo(cartId);
                return Ok(cart);
            }
            catch (CartValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CartNotFoundException ex)
            {
                return NotFound();
            }
            catch (RepositoryException ex)
            {
                return StatusCode(500, new { message = $"An error occurred while processing your request. Please try again later. Error details: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred. Error details: {ex.Message}");
            }
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
            try
            {
                _cartService.AddItemToCart(cartId, item);
                return Ok();
            }
            catch (CartValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RepositoryException ex)
            {
                return StatusCode(500, new { message = $"An error occurred while processing your request. Please try again later. Error details: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred. Error details: {ex.Message}");
            }
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
            try
            {
                _cartService.RemoveItemFromCart(cartId, itemId);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (CartNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (RepositoryException ex)
            {
                return StatusCode(500, new { message = $"An error occurred while processing your request. Please try again later. Error details: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal Server Error. Error details: {ex.Message}" });
            }
        }

        /// <summary>
        /// Returns all carts
        /// </summary>
        /// <returns>Status 200 ok</returns>
        [HttpGet("all")]
        public ActionResult<IEnumerable<Cart>> GetAllCarts()
        {
            try
            {
                var carts = _cartService.GetAllCarts();
                return Ok(carts);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (RepositoryException ex)
            {
                return StatusCode(500, new { message = $"An error occurred while processing your request. Please try again later. Error details: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal Server Error. Error details: {ex.Message}" });
            }
        }
    }
}
