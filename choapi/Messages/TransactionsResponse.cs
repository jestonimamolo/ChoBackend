using choapi.Models;

namespace choapi.Messages
{
    public class TransactionsResponse : ResponseBase
    {
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
