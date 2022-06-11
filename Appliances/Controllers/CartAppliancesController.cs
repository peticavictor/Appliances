using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Appliances.Data;
using Appliances.Models;

namespace Appliances.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartAppliancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartAppliancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CartAppliances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartAppliance>>> GetCartAppliance()
        {
            return await _context.CartAppliance.ToListAsync();
        }

        // GET: api/CartAppliances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartAppliance>> GetCartAppliance(int id)
        {
            var cartAppliance = await _context.CartAppliance.FindAsync(id);

            if (cartAppliance == null)
            {
                return NotFound();
            }

            return cartAppliance;
        }

        // PUT: api/CartAppliances/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartAppliance(int id, CartAppliance cartAppliance)
        {
            if (id != cartAppliance.Id)
            {
                return BadRequest();
            }

            _context.Entry(cartAppliance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartApplianceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CartAppliances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CartAppliance>> PostCartAppliance(CartAppliance cartAppliance)
        {
            _context.CartAppliance.Add(cartAppliance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCartAppliance", new { id = cartAppliance.Id }, cartAppliance);
        }

        // DELETE: api/CartAppliances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartAppliance>> DeleteCartAppliance(int id)
        {
            var cartAppliance = await _context.CartAppliance.FindAsync(id);
            if (cartAppliance == null)
            {
                return NotFound();
            }

            _context.CartAppliance.Remove(cartAppliance);
            await _context.SaveChangesAsync();

            return cartAppliance;
        }

        private bool CartApplianceExists(int id)
        {
            return _context.CartAppliance.Any(e => e.Id == id);
        }
    }
}
