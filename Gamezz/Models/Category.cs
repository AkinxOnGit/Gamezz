namespace Gamezz.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Games> Games { get; set; }
    }
}
