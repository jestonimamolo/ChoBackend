using choapi.Models;

namespace choapi.Messages
{
    public class CardDetailsResponse : ResponseBase
    {
        public CardDetails CardDetails { get; set; } = new CardDetails();
    }
}
