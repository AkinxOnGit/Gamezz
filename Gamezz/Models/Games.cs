namespace Gamezz.Models
{
    public class Games
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Publisher Publisher { get; set; }

        public double Price { get; set; }

        public List<GamesOrders> GamesOrders { get; set; } = new List<GamesOrders>();

        public Pg Pg { get; set; }

        public List<Category> Category { get; set; }
    }
}
