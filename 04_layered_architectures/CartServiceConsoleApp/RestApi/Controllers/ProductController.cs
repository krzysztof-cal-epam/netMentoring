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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetProductsAsync(categoryId, page, pageSize);
            return Ok(products);
        }
    }
}
