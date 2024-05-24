using choapi.Models;

namespace choapi.Messages
{
    public class EstablishmentBookTypeResponse : ResponseBase
    {
        public EstablishmentBookType BookType { get; set; } = new EstablishmentBookType();
    }
}
