using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantImagesResponse : ResponseBase
    {
        public List<RestaurantImages> Images { get; set; } = new List<RestaurantImages>();
    }
}
