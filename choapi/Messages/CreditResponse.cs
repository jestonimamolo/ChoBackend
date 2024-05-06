using choapi.Models;

namespace choapi.Messages
{
    public class CreditResponse : ResponseBase
    {
        public Credits Credit { get; set; } = new Credits();
    }
}
