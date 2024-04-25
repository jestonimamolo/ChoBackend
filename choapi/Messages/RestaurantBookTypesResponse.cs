using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantBookTypesResponse : ResponseBase
    {
        public List<RestaurantBookType> BookTypes { get; set; } = new List<RestaurantBookType>();
    }
}
