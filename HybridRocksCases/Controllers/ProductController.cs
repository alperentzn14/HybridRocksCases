using HybridRocksCases.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HybridRocksCases.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly HybridDbContext _HybridContext;
        public ProductController(HybridDbContext HybridContext)
        {
            _HybridContext = HybridContext;
        }
        //// GETLİST
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetList()
        {
            return await _HybridContext.Products.ToListAsync();
        }

        //// GET
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _HybridContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            var productItem = new Product
            {
                ProductName= product.ProductName,
                Price = product.Price
            };

            _HybridContext.Products.Add(productItem);
            await _HybridContext.SaveChangesAsync();

            return product;
        }


        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var productItem = await _HybridContext.Products.FindAsync(id);
            if (productItem == null)
            {
                return NotFound();
            }

            productItem.ProductName= product.ProductName;
            productItem.Price= product.Price;

            try
            {
                await _HybridContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProductItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // DELETE

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productItem = await _HybridContext.Products.FindAsync(id);

            if (productItem == null)
            {
                return NotFound();
            }

            _HybridContext.Products.Remove(productItem);
            await _HybridContext.SaveChangesAsync();

            return NoContent();
        }
        private bool ProductItemExists(int id)
        {
            return _HybridContext.Products.Any(p => p.Id == id);
        }
    }
}
