using choapi.Models;

namespace choapi.Messages
{
    public class EstablishmentBookTypesResponse : ResponseBase
    {
        public List<EstablishmentBookType> BookTypes { get; set; } = new List<EstablishmentBookType>();
    }
}
