using choapi.Models;

namespace choapi.Messages
{
    public class EstablishmentImagesResponse : ResponseBase
    {
        public List<EstablishmentImages> Images { get; set; } = new List<EstablishmentImages>();
    }
}
