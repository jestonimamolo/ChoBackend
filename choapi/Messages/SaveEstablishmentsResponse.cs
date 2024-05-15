using choapi.Models;

namespace choapi.Messages
{
    public class SaveEstablishmentsResponse : ResponseBase
    {
        public List<SaveEstablishment> SaveEstablishments { get; set; } = new List<SaveEstablishment>();
    }
}
