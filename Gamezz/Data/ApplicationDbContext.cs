using Gamezz.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamezz.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Orders> Orders { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Gamezz.Models.Publisher>? Publisher { get; set; }
        public DbSet<Gamezz.Models.Pg>? Pg { get; set; }
        public DbSet<Gamezz.Models.GamesOrders>? GamesOrders { get; set; }
        public DbSet<Gamezz.Models.Games>? Games { get; set; }
        public DbSet<Gamezz.Models.Category>? Category { get; set; }
    }
}