using choapi.Models;

namespace choapi.DAL
{
    public interface IPromotionDAL
    {
        Promotion Add(Promotion model);

        Promotion Update(Promotion model);

        Promotion Delete(Promotion model);

        Promotion? Get(int id);

        List<Promotion>? GetByEstablishmentId(int id);
    }
}
