using choapi.Models;

namespace choapi.Messages
{
    public class CuisinesResponse : ResponseBase
    {
        public List<Cuisines>? Cuisines { get; set; } = new List<Cuisines>();

    }
}
