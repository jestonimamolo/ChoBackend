using choapi.Models;

namespace choapi.DAL
{
    public class CreditDAL : ICreditDAL
    {
        private readonly ChoDBContext _context;

        public CreditDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Credits Add(Credits model)
        {
            _context.Credits.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Credits Update(Credits model)
        {
            _context.Credits.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Credits Delete(Credits model)
        {
            _context.Credits.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Credits? Get(int id)
        {
            return _context.Credits.FirstOrDefault(c => c.Credit_Id == id && c.Is_Deleted != true);
        }

        public List<Credits>? GetCredits(int id)
        {
            return _context.Credits.Where(c => c.Restaurant_Id == id && c.Is_Deleted != false).ToList();
        }
    }
}
