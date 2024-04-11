using Gamezz.Data;
using Gamezz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gamezz.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<IdentityUser> userList = _context.Users.ToList();

            if (userList == null)
            {
                return NotFound();
            }

            return View(userList);
        }

        /*
        public IActionResult Edit()
        {

        }

        public IActionResult Create(IdentityUser identityUser)
        {

        }
        */

        public IActionResult Delete(String id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
          
            return RedirectToAction("Index");
        }

    }
}
