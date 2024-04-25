using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantAvailabilitiesResponse : ResponseBase
    {
        public List<RestaurantAvailability> Availabilities { get; set; } = new List<RestaurantAvailability>();
    }
}
