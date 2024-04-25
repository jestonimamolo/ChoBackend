using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantBookTypeResponse : ResponseBase
    {
        public RestaurantBookType BookType { get; set; } = new RestaurantBookType();
    }
}
