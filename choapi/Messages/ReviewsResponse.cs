using choapi.Models;

namespace choapi.Messages
{
    public class ReviewsResponse : ResponseBase
    {
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
