using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantCuisineResponse : ResponseBase
    {
        public EstablishmentCuisines RestaurantCuisine { get; set; } = new EstablishmentCuisines();
    }
}
