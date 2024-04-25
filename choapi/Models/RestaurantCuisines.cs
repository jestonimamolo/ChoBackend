using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class RestaurantCuisines
    {
        [Key]
        public int RestaurantCuisine_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public string? Name { get; set; } = null;
    }
}
