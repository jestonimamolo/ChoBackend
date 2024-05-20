using System.ComponentModel.DataAnnotations;

namespace choapi.DTOs
{
    public class RestaurantCuisineDTO
    {
        public int EstablishmentCuisine_Id { get; set; }

        public int Establishment_Id { get; set; }

        public required string Name { get; set; }
    }
}
