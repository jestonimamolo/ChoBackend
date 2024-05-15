using choapi.Models;

namespace choapi.DAL
{
    public interface ISaveEstablishmentDAL
    {
        SaveEstablishment Add(SaveEstablishment model);

        SaveEstablishment Update(SaveEstablishment model);

        SaveEstablishment Delete(SaveEstablishment model);

        SaveEstablishment? Get(int id);

        List<SaveEstablishment>? GetByUserId(int id);
    }
}
