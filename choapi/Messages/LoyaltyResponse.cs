using choapi.Models;

namespace choapi.Messages
{
    public class LoyaltyResponse : ResponseBase
    {
        public Loyalty Loyalty { get; set; } = new Loyalty();
    }
}
