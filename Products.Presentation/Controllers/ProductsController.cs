using Microsoft.AspNetCore.Mvc;
using Products.Business.Services.Interfaces;
using Products.Data.Models;

namespace Products.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is not null)
                return Ok(product);

            return NotFound();

        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            if (product is null)
                return BadRequest();

            var newProduct = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            var updatedProduct = await _productService.UpdateProductAsync(id, product);
            
            if (updatedProduct is not null)
                return Ok(updatedProduct);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
                return NotFound();

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

    }
}
