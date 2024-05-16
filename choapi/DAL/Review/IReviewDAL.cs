using choapi.Models;

namespace choapi.DAL
{
    public interface IReviewDAL
    {
        Review Add(Review model);

        Review Update(Review model);

        Review Delete(Review model);

        Review? Get(int id);

        List<Review>? GetByUserId(int id);
    }
}
