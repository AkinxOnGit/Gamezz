using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamezz.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public IdentityUser User { get; set; }

        public string Invoice { get; set; }

        public List<GamesOrders> GamesOrders { get; set; } = new List<GamesOrders>();

        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
