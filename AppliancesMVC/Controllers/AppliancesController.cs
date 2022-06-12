using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppliancesMVC.Models;
using AppliancesMVC.Data;

namespace Appliances.Controllers
{
    public class AppliancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppliancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appliances
        public async Task<IActionResult> Index()
        {
            return View(await _context.Appliance.ToListAsync());
        }

        // GET: Appliances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appliance = await _context.Appliance
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appliance == null)
            {
                return NotFound();
            }

            return View(appliance);
        }

        // GET: Appliances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appliances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Brand,Model,Code,Price,ProducedOn,Image")] Appliance appliance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appliance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appliance);
        }

        // GET: Appliances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appliance = await _context.Appliance.FindAsync(id);
            if (appliance == null)
            {
                return NotFound();
            }
            return View(appliance);
        }

        // POST: Appliances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Brand,Model,Code,Price,ProducedOn,Image")] Appliance appliance)
        {
            if (id != appliance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appliance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplianceExists(appliance.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appliance);
        }

        // GET: Appliances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appliance = await _context.Appliance
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appliance == null)
            {
                return NotFound();
            }

            return View(appliance);
        }

        // POST: Appliances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appliance = await _context.Appliance.FindAsync(id);
            _context.Appliance.Remove(appliance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplianceExists(int id)
        {
            return _context.Appliance.Any(e => e.Id == id);
        }
    }
}
