namespace Gamezz.Models
{
    public class Pg
    {
        public int Id { get; set; }
        public int AgeRestriction { get; set; }

        public List<Games> Games { get; set; }
    }
}
