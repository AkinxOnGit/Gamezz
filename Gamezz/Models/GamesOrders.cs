using System.ComponentModel.DataAnnotations.Schema;

namespace Gamezz.Models
{
    public class GamesOrders
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        public Orders Orders { get; set; }

        [ForeignKey("Orders")]
        public int OrderId { get; set; }

        [ForeignKey("Games")]
        public int GamesId { get; set; }

        public Games Games { get; set; }
    }
}
