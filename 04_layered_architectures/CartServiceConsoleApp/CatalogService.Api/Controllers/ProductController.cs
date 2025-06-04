using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CatalogService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;

        }

        [HttpGet("list")]
        [Authorize(Policy = "Read")]
        public async Task<IActionResult> GetAll()
        {
            var user = User.Identity.Name ?? "Anonymous";
            var roles = User.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();
            _logger.LogInformation("GetProducts called by {0}. Roles: {1}", user, string.Join(", ", roles));

            var products = await _productService.ListAsync();

            return Ok(products);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "Read")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        [Authorize(Policy = "Create")]
        public async Task<IActionResult> Add([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.AddAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "Update")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != productDto.Id)
                return BadRequest("Product ID mismatch.");

            await _productService.UpdateAsync(productDto);
            return NoContent();
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
