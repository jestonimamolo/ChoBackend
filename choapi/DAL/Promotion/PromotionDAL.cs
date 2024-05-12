using choapi.Models;

namespace choapi.DAL
{
    public class PromotionDAL : IPromotionDAL
    {
        private readonly ChoDBContext _context;

        public PromotionDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Promotion Add(Promotion model)
        {
            _context.Promotion.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Promotion Update(Promotion model)
        {
            _context.Promotion.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Promotion Delete(Promotion model)
        {
            _context.Promotion.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Promotion? Get(int id)
        {
            return _context.Promotion.FirstOrDefault(p => p.Promotion_Id == id && p.Is_Deleted != true);
        }

        public List<Promotion>? GetByEstablishmentId(int id)
        {
            return _context.Promotion.Where(p => p.Establishment_Id == id && p.Is_Deleted != true).ToList();
        }
    }
}
