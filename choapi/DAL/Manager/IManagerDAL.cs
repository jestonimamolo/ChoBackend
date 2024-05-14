using choapi.Models;

namespace choapi.DAL
{
    public interface IManagerDAL
    {
        Manager Add(Manager model);

        Manager Update(Manager model);

        Manager Delete(Manager model);

        Manager? Get(int id);

        List<Manager>? GetByEstablishmentId(int id);
    }
}
