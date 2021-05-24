using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReitanDistribution.Core;
using ReitanDistribution.Infrastructure;

namespace ReitanDistribution.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ReitanDbContext _context;

        public ProductsController(ReitanDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// <para>Url example: GET: Products</para>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products
                //.Include(product => product.Category)
                //.Include(product => product.Supplier)
                //.Include(product => product.Unit)
                .ToListAsync();
            return products;
        }

        /// <summary>
        /// <para>Url example: GET: Products/5</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var foundProduct = await _context.Products
                .Include(product => product.Category)
                .Include(product => product.Supplier)
                .Include(product => product.Unit)
                .FirstOrDefaultAsync(product => product.Id == id);

            if (foundProduct == null)
            {
                return NotFound();
            }

            return foundProduct;
        }

        /// <summary>
        /// <para>Url example: PUT: Products/5</para>
        /// <para>To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            product.Id = id;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return (await PostProduct(product)).Result;
                }

                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// <para>Url example: POST: Products</para>
        /// <para>To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754</para>
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        /// <summary>
        /// <para>Url example: DELETE: Products/5</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
