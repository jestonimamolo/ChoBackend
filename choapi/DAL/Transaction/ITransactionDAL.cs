using choapi.Models;

namespace choapi.DAL
{
    public interface ITransactionDAL
    {
        Transaction Add(Transaction model);

        Transaction Update(Transaction model);

        Transaction Delete(Transaction model);

        Transaction? Get(int id);

        List<Transaction>? GetByUserId(int id);
    }
}
