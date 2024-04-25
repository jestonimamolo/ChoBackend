using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantImageResponse : ResponseBase
    {
        public RestaurantImages Image { get; set; } = new RestaurantImages();
    }
}
