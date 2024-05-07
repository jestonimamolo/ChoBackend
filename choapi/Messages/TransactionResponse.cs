using choapi.Models;

namespace choapi.Messages
{
    public class TransactionResponse : ResponseBase
    {
        public Transaction Transaction { get; set; } = new Transaction();
    }
}
