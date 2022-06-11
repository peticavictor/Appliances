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
    public class AppliancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppliancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Appliances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appliance>>> GetAppliance()
        {
            return await _context.Appliance.ToListAsync();
        }

        // GET: api/Appliances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appliance>> GetAppliance(int id)
        {
            var appliance = await _context.Appliance.FindAsync(id);

            if (appliance == null)
            {
                return NotFound();
            }

            return appliance;
        }

        // PUT: api/Appliances/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppliance(int id, Appliance appliance)
        {
            if (id != appliance.Id)
            {
                return BadRequest();
            }

            _context.Entry(appliance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplianceExists(id))
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

        // POST: api/Appliances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Appliance>> PostAppliance(Appliance appliance)
        {
            _context.Appliance.Add(appliance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppliance", new { id = appliance.Id }, appliance);
        }

        // DELETE: api/Appliances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Appliance>> DeleteAppliance(int id)
        {
            var appliance = await _context.Appliance.FindAsync(id);
            if (appliance == null)
            {
                return NotFound();
            }

            _context.Appliance.Remove(appliance);
            await _context.SaveChangesAsync();

            return appliance;
        }

        private bool ApplianceExists(int id)
        {
            return _context.Appliance.Any(e => e.Id == id);
        }
    }
}
