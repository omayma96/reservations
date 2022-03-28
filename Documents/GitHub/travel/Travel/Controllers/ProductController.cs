using Travel.Models;
using Travel.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("List Products")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productService.GetAll();
        }

        [HttpGet("Product by Id")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _productService.GetById(id);
        }

        [HttpPost("Add Product")]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
            var newProduct = await _productService.Create(product);
            return CreatedAtAction(nameof(GetProducts), new { id = newProduct.ProductID }, newProduct);
        }

        [HttpPut("Update Product")]
        public async Task<ActionResult> PutProduct(int id, [FromBody] Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            await _productService.Update(product);

            return NoContent();
        }

        [HttpDelete("Delete Product")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToDelete = await _productService.GetById(id);
            if (productToDelete == null)
                return NotFound();

            await _productService.Delete(productToDelete.ProductID);
            return NoContent();
        }

    }
}
