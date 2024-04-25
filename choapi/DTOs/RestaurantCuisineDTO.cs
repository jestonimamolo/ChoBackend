using System.ComponentModel.DataAnnotations;

namespace choapi.DTOs
{
    public class RestaurantCuisineDTO
    {
        public int RestaurantCuisine_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public required string Name { get; set; }
    }
}
