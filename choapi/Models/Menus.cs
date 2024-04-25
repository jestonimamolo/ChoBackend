using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Menus
    {
        [Key]
        public int Menu_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public string? Type { get; set; } = null;

        public string? Path { get; set; } = null;
    }
}
