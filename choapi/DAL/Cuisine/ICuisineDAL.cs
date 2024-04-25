using choapi.Models;

namespace choapi.DAL
{
    public interface ICuisineDAL
    {
        Cuisines Add(Cuisines model);

        List<Cuisines>? GetCuicines(int? cuisineId);
    }
}
