using choapi.Models;

namespace choapi.Messages
{
    public class EstablishmentImageResponse : ResponseBase
    {
        public EstablishmentImages Image { get; set; } = new EstablishmentImages();
    }
}
