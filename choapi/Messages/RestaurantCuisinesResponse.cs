using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantCuisinesResponse : ResponseBase
    {
        public List<EstablishmentCuisines>? RestaurantCuisines { get; set; } = new List<EstablishmentCuisines>();
    }
}
