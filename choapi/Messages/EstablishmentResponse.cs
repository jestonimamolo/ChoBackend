using choapi.Models;

namespace choapi.Messages
{
    public class EstablishmentResponse : ResponseBase
    {
        public Establishment Establishment { get; set; } = new Establishment();

        //public List<RestaurantImages>? Images { get; set; } = null;
    }
}
