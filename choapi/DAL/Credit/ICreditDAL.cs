using choapi.Models;

namespace choapi.DAL.Credit
{
    public interface ICreditDAL
    {
        Credits Add(Credits model);

        Credits Update(Credits model);

        Credits Delete(Credits model);

        Credits? Get(int id);

        List<Credits>? GetCredits(int id);
    }
}
