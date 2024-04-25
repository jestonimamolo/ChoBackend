using choapi.Models;

namespace choapi.DAL
{
    public class CuisineDAL : ICuisineDAL
    {
        private readonly ChoDBContext _context;

        public CuisineDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Cuisines Add(Cuisines model)
        {
            _context.Cuisines.Add(model);

            _context.SaveChanges();

            return model;
        }

        public List<Cuisines>? GetCuicines(int? cuisineId)
        {
            if (cuisineId == null)
            {
                return _context.Cuisines.ToList();
            }
            else
            {
                return _context.Cuisines.Where(c => c.Cuisine_Id == cuisineId).ToList();
            }
        }
    }
}
