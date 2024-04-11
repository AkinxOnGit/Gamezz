using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gamezz.Data;
using Gamezz.Models;

namespace Gamezz.Controllers
{
    public class GamesOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GamesOrders
        public async Task<IActionResult> Index()
        {
              return _context.GamesOrders != null ? 
                          View(await _context.GamesOrders.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GamesOrders'  is null.");
        }

        // GET: GamesOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GamesOrders == null)
            {
                return NotFound();
            }

            var gamesOrders = await _context.GamesOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamesOrders == null)
            {
                return NotFound();
            }

            return View(gamesOrders);
        }

        // GET: GamesOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GamesOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount")] GamesOrders gamesOrders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gamesOrders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gamesOrders);
        }

        // GET: GamesOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GamesOrders == null)
            {
                return NotFound();
            }

            var gamesOrders = await _context.GamesOrders.FindAsync(id);
            if (gamesOrders == null)
            {
                return NotFound();
            }
            return View(gamesOrders);
        }

        // POST: GamesOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount")] GamesOrders gamesOrders)
        {
            if (id != gamesOrders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamesOrders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamesOrdersExists(gamesOrders.Id))
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
            return View(gamesOrders);
        }

        // GET: GamesOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GamesOrders == null)
            {
                return NotFound();
            }

            var gamesOrders = await _context.GamesOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamesOrders == null)
            {
                return NotFound();
            }

            return View(gamesOrders);
        }

        // POST: GamesOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GamesOrders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GamesOrders'  is null.");
            }
            var gamesOrders = await _context.GamesOrders.FindAsync(id);
            if (gamesOrders != null)
            {
                _context.GamesOrders.Remove(gamesOrders);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamesOrdersExists(int id)
        {
          return (_context.GamesOrders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
