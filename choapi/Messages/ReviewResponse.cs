using choapi.Models;

namespace choapi.Messages
{
    public class ReviewResponse : ResponseBase
    {
        public Review Review { get; set; } = new Review();
    }
}
