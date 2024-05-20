using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantAvailabilitiesResponse : ResponseBase
    {
        public List<Availability> Availabilities { get; set; } = new List<Availability>();
    }
}
