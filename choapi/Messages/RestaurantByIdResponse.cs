using choapi.Models;

namespace choapi.Messages
{
    public class RestaurantByIdResponse : ResponseBase
    {
        public Restaurants Restaurant { get; set; } = new Restaurants();

        public List<EstablishmentImages>? Images { get; set; } = null;
    }
}
