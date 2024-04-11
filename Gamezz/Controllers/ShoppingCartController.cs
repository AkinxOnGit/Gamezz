using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gamezz.Data;
using Gamezz.Models;
using Microsoft.AspNetCore.Identity;

namespace Gamezz.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // HAVE TO IMPLEMENT BUY METHOD TO CREATE ORDER AFTER PRESSING BUY BUTTON IN SHOPPING CART


        // GET: ShoppingCart
        public async Task<IActionResult> Index()
        {   
              var user = await _userManager.GetUserAsync(User);

           var cartList = _context.GamesOrders
              .Include(x => x.Orders)
              .Include(x => x.Games)
              .Where(go => go.Orders.UserId == user.Id)
              .Where(go => go.Orders.Invoice == "");
               
              return _context.GamesOrders != null ? 
                          View(cartList) :
                          Problem("Entity set 'ApplicationDbContext.GamesOrders'  is null.");
        }

        public IActionResult Buy(int gameId, int orderId)
        {
            IdentityUser currentUser = _userManager.GetUserAsync(User).Result;
            var game = _context.Games.FirstOrDefault(g => g.Id == gameId);

            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            order.Invoice = "Rechnung für " + game.Name;

            _context.Update(order);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: ShoppingCart/Details/5
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

        // GET: ShoppingCart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCart/Create
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

        // GET: ShoppingCart/Edit/5
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

        // POST: ShoppingCart/Edit/5
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

        // GET: ShoppingCart/Delete/5
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

        // POST: ShoppingCart/Delete/5
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
