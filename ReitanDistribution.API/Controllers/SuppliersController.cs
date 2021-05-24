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
    public class SuppliersController : ControllerBase
    {
        private readonly ReitanDbContext _context;

        public SuppliersController(ReitanDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// <para>Url example: GET: Suppliers</para>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        /// <summary>
        /// <para>Url example: GET: Suppliers/5</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }

        /// <summary>
        /// <para>Url example: PUT: Suppliers/5</para>
        /// <para>To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 </para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="supplier"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(Guid id, Supplier supplier)
        {
            supplier.Id = id;

            _context.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
                {
                    return (await PostSupplier(supplier)).Result;
                }

                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// <para>Url example: POST: Suppliers/5</para>
        /// <para>To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 </para>
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplier", new { id = supplier.Id }, supplier);
        }

        /// <summary>
        /// <para>Url example: DELETE: Suppliers/5</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierExists(Guid id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }
    }
}
