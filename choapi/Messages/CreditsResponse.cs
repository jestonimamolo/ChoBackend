using choapi.Models;

namespace choapi.Messages
{
    public class CreditsResponse : ResponseBase
    {
        public List<Credits> Credits { get; set; } = new List<Credits>();
    }
}
