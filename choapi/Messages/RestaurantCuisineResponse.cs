using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantCuisineResponse : ResponseBase
    {
        public RestaurantCuisines RestaurantCuisine { get; set; } = new RestaurantCuisines();
    }
}
