using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantCuisinesResponse : ResponseBase
    {
        public List<RestaurantCuisines>? RestaurantCuisines { get; set; } = new List<RestaurantCuisines>();
    }
}
