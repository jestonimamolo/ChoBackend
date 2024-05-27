using choapi.Models;

namespace choapi.Messages
{
    public class EstablishmentAvailabilityResponse : ResponseBase
    {
        public Availability Availability { get; set; } = new Availability();
    }
}
