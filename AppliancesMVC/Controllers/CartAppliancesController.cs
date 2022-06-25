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
    public class CartAppliancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartAppliancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CartAppliances
        public async Task<IActionResult> Index()
        {
            ViewBag.LoggedUser1 = Request.Cookies["LoggedUser1"];
            var user = _context.User.FirstOrDefault(user => user.Name == Request.Cookies["LoggedUser1"]);
            var cartAppliances = _context.CartAppliance.Where(cartAppliance => cartAppliance.CartId == -1);

            if (user == null)
            {
                ViewBag.NrProduse = 0;
            }
            else
            {
                var cart = _context.Cart.FirstOrDefault(cart => cart.IsPayed == 0 && cart.UserId == user.Id);
                if (cart != null)
                {
                    ViewBag.NrProduse = _context.CartAppliance.Where(cartAppliance => cartAppliance.CartId == cart.Id).Count();
                    cartAppliances = _context.CartAppliance.Include(c => c.Cart).Include(a => a.Appliance).Where( x => x.CartId == cart.Id);
                }
                else
                {
                    ViewBag.NrProduse = 0;
                }
            }
            return View(await cartAppliances.ToListAsync());
        }

        // GET: CartAppliances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartAppliance = await _context.CartAppliance
                .Include(c => c.Appliance)
                .Include(c => c.Cart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartAppliance == null)
            {
                return NotFound();
            }

            return View(cartAppliance);
        }

        // GET: CartAppliances/Create
        public IActionResult Create()
        {
            ViewData["ApplianceId"] = new SelectList(_context.Appliance, "Id", "Id");
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id");
            return View();
        }

        // POST: CartAppliances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,CartId,ApplianceId")] CartAppliance cartAppliance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartAppliance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplianceId"] = new SelectList(_context.Appliance, "Id", "Id", cartAppliance.ApplianceId);
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", cartAppliance.CartId);
            return View(cartAppliance);
        }

        // GET: CartAppliances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartAppliance = await _context.CartAppliance.FindAsync(id);
            if (cartAppliance == null)
            {
                return NotFound();
            }
            ViewData["ApplianceId"] = new SelectList(_context.Appliance, "Id", "Id", cartAppliance.ApplianceId);
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", cartAppliance.CartId);
            return View(cartAppliance);
        }

        // POST: CartAppliances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,CartId,ApplianceId")] CartAppliance cartAppliance)
        {
            if (id != cartAppliance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartAppliance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartApplianceExists(cartAppliance.Id))
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
            ViewData["ApplianceId"] = new SelectList(_context.Appliance, "Id", "Id", cartAppliance.ApplianceId);
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", cartAppliance.CartId);
            return View(cartAppliance);
        }

        // GET: CartAppliances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartAppliance = await _context.CartAppliance
                .Include(c => c.Appliance)
                .Include(c => c.Cart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartAppliance == null)
            {
                return NotFound();
            }

            return View(cartAppliance);
        }

        // POST: CartAppliances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartAppliance = await _context.CartAppliance.FindAsync(id);
            _context.CartAppliance.Remove(cartAppliance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartApplianceExists(int id)
        {
            return _context.CartAppliance.Any(e => e.Id == id);
        }

        // Pay:
        public async Task<IActionResult> PayAsync()
        {
            var user = _context.User.FirstOrDefault(x => x.Name == Request.Cookies["LoggedUser1"]);

            if (user != null)
            {
                ViewBag.NrProduse = 0;
                var cart = _context.Cart.FirstOrDefault(x => x.IsPayed == 0 && x.UserId == user.Id);
                if (cart != null)
                {
                    cart.IsPayed = 1;
                    _context.Update(cart);
                    var cartAppliances = _context.CartAppliance.Include(a => a.Appliance).Where(x => x.CartId == cart.Id);

                    foreach(var cartAppliance in cartAppliances)
                    {
                        cartAppliance.Appliance.Quantity -= cartAppliance.Quantity;
                        _context.Appliance.Update(cartAppliance.Appliance);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
