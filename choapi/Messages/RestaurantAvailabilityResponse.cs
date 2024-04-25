using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantAvailabilityResponse : ResponseBase
    {
        public RestaurantAvailability Availability { get; set; } = new RestaurantAvailability();
    }
}
