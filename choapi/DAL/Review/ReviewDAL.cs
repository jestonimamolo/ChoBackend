using choapi.Models;

namespace choapi.DAL
{
    public class ReviewDAL : IReviewDAL
    {
        private readonly ChoDBContext _context;

        public ReviewDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Review Add(Review model)
        {
            _context.Review.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Review Update(Review model)
        {
            _context.Review.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Review Delete(Review model)
        {
            _context.Review.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Review? Get(int id)
        {
            return _context.Review.FirstOrDefault(s => s.Review_Id == id && s.Is_Deleted != true);
        }

        public List<Review>? GetByUserId(int id)
        {
            return _context.Review.Where(s => s.User_Id == id && s.Is_Deleted != true).ToList();
        }
    }
}
