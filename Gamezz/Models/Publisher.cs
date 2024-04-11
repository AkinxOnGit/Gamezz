using System.ComponentModel.DataAnnotations;

namespace Gamezz.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public List<Games> Games { get; set; } = new List<Games>();

    }
}
