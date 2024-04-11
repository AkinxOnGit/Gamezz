using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gamezz.Data;
using Gamezz.Models;
using Microsoft.AspNetCore.Authorization;


namespace Gamezz.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;

	[Authorize(Roles = "Admin")]
	public class GamesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;


		public GamesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}


		[AllowAnonymous]
		// GET: Games
		public async Task<IActionResult> Index()
		{
			var games = await _context.Games.Include(g => g.Publisher).Include(g => g.Pg).ToListAsync();
			return _context.Games != null ?
						View(games) :
						Problem("Entity set 'ApplicationDbContext.Games'  is null.");
		}

		public IActionResult Buy(int gameId)
		{
			var game = _context.Games.FirstOrDefault(g => g.Id == gameId);

			IdentityUser currentUser = _userManager.GetUserAsync(User).Result;

			var order = new Orders
			{
				UserId = currentUser.Id,
				Invoice = "",
				Date = DateTime.Now,
			};

			var gamePrice = game.Price;

			_context.Orders.Add(order);
			_context.SaveChanges();


			var gameOrder = new GamesOrders
			{
				GamesId = gameId,
				OrderId = order.Id,
				Amount = 1,
			};

		

			_context.GamesOrders.Add(gameOrder);
			_context.SaveChanges();

			return RedirectToAction("Index");

		}

		// GET: Games/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Games == null)
			{
				return NotFound();
			}

			var games = await _context.Games
				.FirstOrDefaultAsync(m => m.Id == id);
			if (games == null)
			{
				return NotFound();
			}

			return View(games);
		}

		// GET: Games/Create
		public IActionResult Create()
		{
			List<Publisher> publisher = _context.Publisher.ToList();
			List<Pg> pg = _context.Pg.ToList();
			List<Category> category = _context.Category.ToList();

			ViewBag.Publisher = publisher;
			ViewBag.Pg = pg;
			ViewBag.Category = category;

			return View();
		}



		// POST: Games/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Games games)
		{

			var publisher = await _context.Publisher.FindAsync(games.Publisher!.Id);
			var pg = await _context.Pg.FindAsync(games.Pg!.Id);

			games.Publisher = publisher;
			games.Pg = pg;


			_context.Add(games);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));

			return View(games);
		}

		// GET: Games/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Games == null)
			{
				return NotFound();
			}

			var games = await _context.Games.FindAsync(id);
			if (games == null)
			{
				return NotFound();
			}
			return View(games);
		}

		// POST: Games/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Games games)
		{
			if (id != games.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(games);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!GamesExists(games.Id))
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
			return View(games);
		}

		// GET: Games/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Games == null)
			{
				return NotFound();
			}

			var games = await _context.Games
				.FirstOrDefaultAsync(m => m.Id == id);
			if (games == null)
			{
				return NotFound();
			}

			return View(games);
		}

		// POST: Games/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Games == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Games'  is null.");
			}
			var games = await _context.Games.FindAsync(id);
			if (games != null)
			{
				_context.Games.Remove(games);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool GamesExists(int id)
		{
			return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
