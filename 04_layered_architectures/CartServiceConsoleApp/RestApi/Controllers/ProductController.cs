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
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetProductsAsync(categoryId, page, pageSize);
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
    }
}
