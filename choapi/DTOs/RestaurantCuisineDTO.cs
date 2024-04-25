using System.ComponentModel.DataAnnotations;

namespace choapi.DTOs
{
    public class RestaurantCuisineDTO
    {
        public required int Restaurant_Id { get; set; }

        public required string Name { get; set; }
    }
}
