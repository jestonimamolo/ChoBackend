using choapi.Models;

namespace choapi.Messages
{
    public class EstablishmentAvailabilitiesResponse : ResponseBase
    {
        public List<Availability> Availabilities { get; set; } = new List<Availability>();
    }
}
