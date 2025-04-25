using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var selfLink = Url.Action(nameof(GetById), "Product", new { id = product.Id }, Request.Scheme, Request.Host.Value);
            var updateLink = Url.Action(nameof(Update), "Product", new { id = product.Id }, Request.Scheme, Request.Host.Value);
            var deleteLink = Url.Action(nameof(Delete), "Product", new { id = product.Id }, Request.Scheme, Request.Host.Value);

            Response.Headers.Append("Link", $"<{selfLink}>; rel=\"self\"; method=\"GET\"");
            Response.Headers.Append("Link", $"<{updateLink}>; rel=\"update\"; method=\"PUT\"");
            Response.Headers.Append("Link", $"<{deleteLink}>; rel=\"delete\"; method=\"DELETE\"");

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Amount = product.Amount,
                CategoryId = product.CategoryId
            };

            return Ok(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> ListProductsByCategory([FromQuery] int? categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.ListAsync(categoryId, page, pageSize);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDto productDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var res = await _productService.AddAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id)
                return BadRequest("Product ID in the URL does not match the ID of the product.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.UpdateAsync(productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
