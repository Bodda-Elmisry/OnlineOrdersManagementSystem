using Microsoft.AspNetCore.Mvc;
using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Models.sal;
using OnlineOrderManagementSystem.Repository.IServices.Sal;

namespace OnlineOrderManagementSystem.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetProductsDTO dto)
        {
            var products = await _productService.GetAllProductsAsync(dto);
            return Ok(products);
        }

        [HttpGet("products/{Id}")]
        public async Task<IActionResult> GetProduct(long Id)
        {
            var product = await _productService.GetProductByIdAsync(Id);
            return Ok(product);
        }

        [HttpPost("products")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDTO product)
        {
            var added = await _productService.AddProductAsync(product);
            return Ok(added);
        }

        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var updated = await _productService.UpdateProductAsync(product);
            return Ok(updated);
        }

    }
}
