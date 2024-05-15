using choapi.Models;

namespace choapi.Messages
{
    public class SaveEstablishmentResponse : ResponseBase
    {
        public SaveEstablishment SaveEstablishment { get; set; } = new SaveEstablishment();
    }
}
