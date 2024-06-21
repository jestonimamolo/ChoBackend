using choapi.Models;

namespace choapi.Messages
{
    public class LoyaltiesResponse : ResponseBase
    {
        public List<Loyalty>? loyalties {  get; set; } = new List<Loyalty>();
    }
}
