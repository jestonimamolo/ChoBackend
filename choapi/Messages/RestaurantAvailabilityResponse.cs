using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantAvailabilityResponse : ResponseBase
    {
        public Availability Availability { get; set; } = new Availability();
    }
}
