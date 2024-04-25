using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Cuisines
    {
        [Key]
        public int Cuisine_Id { get; set; }

        public string? Title { get; set; } = string.Empty;
    }
}
