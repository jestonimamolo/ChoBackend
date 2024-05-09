using choapi.Models;

namespace choapi.Messages
{
    public class PromotionResponse : ResponseBase
    {
        public Promotion Promotion { get; set; } = new Promotion();
    }
}
