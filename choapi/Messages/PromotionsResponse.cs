using choapi.Models;

namespace choapi.Messages
{
    public class PromotionsResponse : ResponseBase
    {
        public List<Promotion> Promotions { get; set; } = new List<Promotion>();
    }
}
