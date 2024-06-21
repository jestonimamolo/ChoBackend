using choapi.Models;

namespace choapi.DAL
{
    public interface ILoyaltyDAL
    {
        Loyalty Add(Loyalty model);

        Loyalty Update(Loyalty model);

        Loyalty Delete(Loyalty model);

        Loyalty? Get(int id);

        List<Loyalty>? GetByUserId(int id);
    }
}
