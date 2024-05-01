using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantResponse : ResponseBase
    {
        public Restaurants Restaurant { get; set; } = new Restaurants();

        //public List<RestaurantImages>? Images { get; set; } = null;
    }
}
