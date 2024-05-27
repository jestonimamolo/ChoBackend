using choapi.Models;

namespace choapi.Messages
{
    public class EstablishmentByIdResponse : ResponseBase
    {
        public Establishment Establishment { get; set; } = new Establishment();

        public List<EstablishmentImages>? Images { get; set; } = null;
    }
}
