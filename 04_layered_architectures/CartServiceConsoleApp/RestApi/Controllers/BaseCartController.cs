using CartServiceConsoleApp.DAL.Exceptions;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    public abstract class BaseCartController : ControllerBase
    {
        protected readonly ICartService _cartService;

        protected BaseCartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        protected ActionResult HandleOperation<T>(Func<T> operation)
        {
            try
            {
                var result = operation();
                return Ok(result);
            }
            catch (CartValidationException ex)
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

        protected IActionResult HandleOperation(Action operation)
        {
            try
            {
                operation();
                return Ok();
            }
            catch (CartValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (CartNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (RepositoryException ex)
            {
                return StatusCode(500, new { message = $"An error occurred while processing your request. Details: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal Server Error. Details: {ex.Message}" });
            }
        }
    }
}
