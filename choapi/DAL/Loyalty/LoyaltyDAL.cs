using choapi.Models;
using Microsoft.EntityFrameworkCore;

namespace choapi.DAL
{
    public class LoyaltyDAL : ILoyaltyDAL
    {
        private readonly ChoDBContext _context;

        public LoyaltyDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Loyalty Add(Loyalty model)
        {
            _context.Loyalty.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Loyalty Delete(Loyalty model)
        {
            _context.Loyalty.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Loyalty? Get(int id)
        {
            return _context.Loyalty.FirstOrDefault(m => m.Loyalty_Id == id && m.Is_Deleted != true);
        }

        public List<Loyalty>? GetByUserId(int id)
        {
            return _context.Loyalty.Where(m => m.User_Id == id && m.Is_Deleted != true).ToList();
        }

        public Loyalty Update(Loyalty model)
        {
            _context.Loyalty.Update(model);

            _context.SaveChanges(true);

            return model;
        }
    }
}
